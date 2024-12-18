using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
    }

}
