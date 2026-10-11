using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderApi.Models;
using OrderApi.Services.Interface;

namespace OrderApi.Controllers
{
    
    [ApiController]
    [EnableRateLimiting("fixed-policy")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
       
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,  IOrderService orderService)
        {
            _logger = logger;
            
            _orderService = orderService;
        }
        [HttpGet]

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync(CancellationToken.None);
            return Ok(orders);
        }
    }

    [ApiController]
    [EnableRateLimiting("fixed-policy")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Order")]
    public class OrderV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            // Replace with the actual V2 service and DTO.
            return Ok(new
            {
                Version = "2.0",
                Message = "Order API version 2"
            });
        }
    }
    }
