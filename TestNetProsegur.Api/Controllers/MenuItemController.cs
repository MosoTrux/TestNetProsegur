using Microsoft.AspNetCore.Mvc;
using TestNetProsegur.Application.Implements;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _menuItemService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(MenuItem entity)
        //{
        //    var result = await _menuItemService.Register(entity);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpPut("Update")]
        //public async Task<IActionResult> Update(MenuItem entity)
        //{
        //    var result = await _menuItemService.Update(entity);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
    }
}
