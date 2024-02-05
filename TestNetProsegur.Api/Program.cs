using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TestNetProsegur.Api.Middlewares;
using TestNetProsegur.Application.Enums;
using TestNetProsegur.Application.Implements;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using TestNetProsegur.Core.Repositories.Mockapi.io;
using TestNetProsegur.Infrastructure.DBContexts;
using TestNetProsegur.Infrastructure.Repositories;
using TestNetProsegur.Infrastructure.Repositories.Mockapi.io;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Log/application.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddMemoryCache();

builder.Services
    .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDb"));

builder.Services
    .AddDbContext<AuthenticationDbContext>(options => options.UseInMemoryDatabase("AuthDb"));

builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

// Setup identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AuthenticationDbContext>();


// Add JWTs
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IMockapiIORespository, MockapiIORespository>();
builder.Services.AddScoped<IStartupInitializer, StartupInitializer>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IMenuItemService, MenuItemService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IBillingService, BillingService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IMockapiIOService, MockapiIOService>();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

var app = builder.Build();
app.UseMiddleware<PerfomanceLoggingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync(RolesEnum.ADMINISTRATOR.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(RolesEnum.ADMINISTRATOR.ToString()));
    }
    if (!await roleManager.RoleExistsAsync(RolesEnum.SUPERVISOR.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(RolesEnum.SUPERVISOR.ToString()));
    }
    if (!await roleManager.RoleExistsAsync(RolesEnum.EMPLOYEED.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(RolesEnum.EMPLOYEED.ToString()));
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Inicializar datos
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
    await initializer.Initialize();
}

app.Run();
Log.CloseAndFlush();
