using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Infrastructure.DBContexts
{
    public class AuthenticationDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
