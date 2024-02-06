using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestNetProsegur.Application.Dtos.Order;
using TestNetProsegur.Application.Interfaces;

namespace TestNetProsegur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBillingService _billingService;        
        public OrderController(IOrderService orderService, IBillingService billingService)
        {
            _orderService = orderService;
            _billingService = billingService;
        }

        //[HttpGet("Cancel")]
        //public async Task<IActionResult> Cancel(long id)
        //{
        //    var result = await _orderService.Cancel(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        [HttpGet("GetAll")]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _orderService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Register")]
        [Authorize(Roles = "ADMINISTRATOR, EMPLOYEED")]
        public async Task<IActionResult> Register(RegisterOrderDto model)
        {
            var result = await _orderService.Register(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetInvoice")]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR")]
        public async Task<IActionResult> GetInvoice(long orderId)
        {
            var result = await _billingService.GetInvoice(orderId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
