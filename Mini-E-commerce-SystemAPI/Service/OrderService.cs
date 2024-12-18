using Dapper;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;
using System.Data;

namespace Mini_E_commerce_SystemAPI.Service
{
    public class OrderService : IOrderService
    {
        private readonly IDbConnection _dbConnection;

        public OrderService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Create a new order and insert related order items
        public async Task<Order> CreateOrderAsync(int userId)
        {
            var cartItems = await _dbConnection.QueryAsync<ShoppingCartItem>("SELECT * FROM ShoppingCart WHERE UserId = @UserId", new { UserId = userId });

            if (!cartItems.Any())
            {
                throw new Exception("Cart is empty.");
            }

            var totalAmount = cartItems.Sum(item => item.Quantity * item.Price);
            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Status = "Pending"
            };

            var orderId = await _dbConnection.ExecuteScalarAsync<int>("INSERT INTO Orders (UserId, TotalAmount, Status) OUTPUT INSERTED.OrderId VALUES (@UserId, @TotalAmount, @Status)", order);
            order.OrderId = orderId;

            // Add order items
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price
                };

                await _dbConnection.ExecuteAsync("INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) VALUES (@OrderId, @ProductId, @Quantity, @Price)", orderItem);
            }

            // Clear cart after order creation
            await _dbConnection.ExecuteAsync("DELETE FROM ShoppingCart WHERE UserId = @UserId", new { UserId = userId });

            return order;
        }

        // Get an order by ID
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var query = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(query, new { OrderId = orderId });
        }

        // Get all items in an order
        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            var query = "SELECT * FROM OrderItems WHERE OrderId = @OrderId";
            return await _dbConnection.QueryAsync<OrderItem>(query, new { OrderId = orderId });
        }
    }

}
