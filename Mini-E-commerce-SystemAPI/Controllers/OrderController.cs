using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;
using Mini_E_commerce_SystemAPI.Service;

namespace Mini_E_commerce_SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        //Method to add order 
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(int userId)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(userId);
                return Ok(new
                {
                    Message = "Order created successfully.",
                    OrderDetails = order
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Failed to create order.",
                    Error = ex.Message
                });
            }

        }
    }
}
