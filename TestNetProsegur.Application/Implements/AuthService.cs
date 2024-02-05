using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Auth;
using TestNetProsegur.Application.Interfaces;

namespace TestNetProsegur.Application.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponseDto<string>> AddRoles(AddRolesDto model)
        {
            var response = new ServiceResponseDto<string>();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new Exception("Usuario no existe.");
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                List<string> roles = model.GetRoles()!.Except(currentRoles).ToList();

                if (!roles.Any()) 
                {
                    throw new Exception("Los usuarios ya tiene los roles asignados.");
                }

                var assignRoleResult = await _userManager.AddToRolesAsync(user, roles);

                if (!assignRoleResult.Succeeded)
                {
                    throw new Exception("Error en la asignación de los roles.");
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo asignar los roles. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<string>> GetLoggedInUser(ClaimsPrincipal User)
        {
            var response = new ServiceResponseDto<string>();
            try
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByNameAsync(username);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo asignar los roles. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<LoginResultDto>> Login(LoginDto model)
        {
            var response = new ServiceResponseDto<LoginResultDto>();
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    throw new Exception("Email o contraseña incorrecta.");
                }

                var user = await _userManager.FindByNameAsync(model.Email);

                response.Data = new LoginResultDto
                {
                    Email = model.Email,
                    Roles = await _userManager.GetRolesAsync(user),
                    Token = await _tokenService.GenerateJwtToken(user, TimeSpan.FromMinutes(30)),
                    //Token = await _tokenService.GenerateJwtToken(user, TimeSpan.FromMinutes(1))
                };
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"Login fallido. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<string>> Register(RegisterDto model)
        {
            var response = new ServiceResponseDto<string>();
            try
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Error en el registro.");
                }

                var assignRoleResult = await _userManager.AddToRolesAsync(user, model.GetRoles());

                if (!assignRoleResult.Succeeded)
                {
                    throw new Exception("Error en la asignación de los roles.");
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo registrar el usuario. Exception: {ex.Message}");
            }
            return response;
        }
    }
}
