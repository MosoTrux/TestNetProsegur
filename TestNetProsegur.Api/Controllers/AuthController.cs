using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestNetProsegur.Application.Dtos.Auth;
using TestNetProsegur.Application.Enums;
using TestNetProsegur.Application.Interfaces;

namespace TestNetProsegur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authService.Register(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost("AddRoles")]
        //[AllowAnonymous]
        //public async Task<IActionResult> AddRoles([FromBody] AddRolesDto model)
        //{
        //    var result = await _authService.AddRoles(model);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.Login(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost("Logout")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Logout([FromBody] LoginDto model)
        //{
        //    var result = await _authService.Login(model);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpGet("user")]
        //[Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, EMPLOYEED")]
        //public async Task<ActionResult<IdentityUser>> GetLoggedInUser()
        //{
        //    var result = await _authService.GetLoggedInUser(User);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
    }
}
