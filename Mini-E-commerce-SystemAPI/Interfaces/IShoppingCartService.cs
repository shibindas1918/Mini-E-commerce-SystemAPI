using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(ShoppingCartItem item);
        Task RemoveFromCartAsync(ShoppingCartItem item);
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(int userId);
    }
}
