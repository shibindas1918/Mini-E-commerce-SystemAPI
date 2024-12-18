using Microsoft.AspNetCore.Mvc;
using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Interfaces
{
    public interface IProductService
    {
         Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync([FromBody] Product product);
    }
}
