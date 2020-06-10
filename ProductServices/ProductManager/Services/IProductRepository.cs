using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IProductRepository
    {
        Task<ProductItem> GetAsync(int productNumber);
        Task<List<ProductItem>> ListAllAsync();
        Task<ProductItem> AddAsync(ProductItem productItem);
        Task UpdateAsync(ProductItem productItem);
        Task DeleteAsync(ProductItem productItem);
    }
}