using ProductManager.Data;

namespace ProductManager.Services
{
    public interface IProductService
    {
        void UpdateState(ProductDto productDto);
    }
}