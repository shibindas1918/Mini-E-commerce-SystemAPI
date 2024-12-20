using Dapper;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;
using System.Data;

namespace Mini_E_commerce_SystemAPI.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDbConnection _dbConnection;

        public ShoppingCartService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        // Service for Adding products into the shoppingcart 
        public async Task AddToCartAsync(ShoppingCartItem item)
        {
            await _dbConnection.ExecuteAsync("INSERT INTO ShoppingCart (UserId, Quantity,price) VALUES (@UserId, @Quantity, @price)", item);

        }
        //Service for deleting cart from the shopping cart 
        public async Task RemoveFromCartAsync(ShoppingCartItem item)
        {
            await _dbConnection.ExecuteAsync("DELETE FROM ShoppingCart WHERE CartId = @CartId", item);
        }
        //Service for feting the shoppingcart details of a particular user 
        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(int userId)
        {
            return await _dbConnection.QueryAsync<ShoppingCartItem>("SELECT * FROM ShoppingCart WHERE UserId = @UserId", new { UserId = userId });
        }
    }

}
