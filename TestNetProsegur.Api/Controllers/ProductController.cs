using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestNetProsegur.Application.Dtos.ProductDto;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;
        public ProductController(IProductService productService, IStockService stockService)
        {
            _productService = productService;
            _stockService = stockService;
        }

        //[HttpPost("Add")]
        //public async Task<IActionResult> Add(Product entity)
        //{
        //    var result = await _productService.Add(entity);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _productService.Delete(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        [HttpGet("GetAll")]
        //[Authorize(Roles = "SUPERVISOR, EMPLOYEED")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpGet("GetById")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result = await _productService.GetById(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        [HttpPut("AddStock")]
        public async Task<IActionResult> AddStock(List<AddStockDto> model)
        {
            var result = await _stockService.AddStock(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
