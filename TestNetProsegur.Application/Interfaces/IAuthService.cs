using System.Security.Claims;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Auth;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponseDto<string>> AddRoles(AddRolesDto model);
        Task<ServiceResponseDto<string>> GetLoggedInUser(ClaimsPrincipal User);
        Task<ServiceResponseDto<LoginResultDto>> Login(LoginDto model);
        Task<ServiceResponseDto<string>> Register(RegisterDto model);
    }
}
