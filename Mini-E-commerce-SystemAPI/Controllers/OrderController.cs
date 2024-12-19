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
        [HttpPost]
        public IActionResult CreateOrders(int userid )
        {
           var order =  _orderService.CreateOrderAsync(userid);
            return Ok (order);  

        }

    }
}
