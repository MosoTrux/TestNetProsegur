using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Infrastructure.DBContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();

            modelBuilder.Entity<MenuItem>()
            .HasMany(m => m.Ingredients)
            .WithOne(d => d.IdMenuItemNavigation)
            .HasForeignKey(d => d.IdMenuItem);

            modelBuilder.Entity<MenuItem>()
            .HasMany(m => m.OrderItems)
            .WithOne(d => d.IdMenuItemNavigation)
            .HasForeignKey(d => d.IdMenuItem);

            modelBuilder.Entity<Order>()
            .HasMany(m => m.OrderItems)
            .WithOne(d => d.IdOrderNavigation)
            .HasForeignKey(d => d.IdOrder);
        }
    }
}
