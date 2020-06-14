using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Data;

namespace ProductManager.Services
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int productNumber);
        Task<List<Product>> ListAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int productNumber);
    }
}