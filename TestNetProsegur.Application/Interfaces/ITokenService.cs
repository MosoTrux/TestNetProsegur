using Microsoft.AspNetCore.Identity;

namespace TestNetProsegur.Application.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateJwtToken(IdentityUser user, TimeSpan expiration);
    }
}
