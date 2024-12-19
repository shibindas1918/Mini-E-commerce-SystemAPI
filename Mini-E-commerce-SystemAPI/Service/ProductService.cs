using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Mini_E_commerce_SystemAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IDbConnection _dbConnection;
        public ProductService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbConnection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE ProductId = @Id", new { Id = id });
        }
        public async Task AddProductAsync([FromBody] Product product)
        {
            await _dbConnection.ExecuteAsync("Insert into products (name,Description,Price,StockQuantity)values (@name,@Description,@Price,@StockQuantity)", product);
        }
    }
}
