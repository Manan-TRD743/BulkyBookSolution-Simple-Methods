using BulkyBook_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook_WebAPI.Services
{
  /*  public interface IProduct : IServices<Product>
    {
        void UpdateProduct(Product product);
    }*/
    
    public interface IProduct 
    {
        Task AddProductAsync(Product Product, IFormFile file);
        Task UpdateProductAsync(Product Product,IFormFile file);
        Task DeleteProductAsync(Product Product);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductAsync(int? ProductID);
        Task SaveProductAsync();

    }
}