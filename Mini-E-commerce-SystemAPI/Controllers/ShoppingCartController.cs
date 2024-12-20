using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCart;
        public ShoppingCartController(IShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;

        }

        //Method to Add products to cart 
        [HttpPost]
        public ActionResult AddCart(ShoppingCartItem cart)
        {
            _shoppingCart.AddToCartAsync(cart);
            return Ok("Item has been Added ");
        }
        //Method to Delete a Item from the cart 
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] ShoppingCartItem item)
        {
            await _shoppingCart.RemoveFromCartAsync(item);
            return Ok();
        }
        //Method To Get a user by particular Id 
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cartItems = await _shoppingCart.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }

    }

}