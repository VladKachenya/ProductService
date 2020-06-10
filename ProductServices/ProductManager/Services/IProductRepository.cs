using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IProductRepository
    {
        Task<ProductItem> GetAsync(int productNumber);
        Task<IReadOnlyCollection<ProductItem>> ListAllAsync();
        Task<ProductItem> AddAsync(ProductItem entity);
        Task UpdateAsync(ProductItem entity);
        Task DeleteAsync(ProductItem entity);
    }
}