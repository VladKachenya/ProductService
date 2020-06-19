using ProductManager.Data;

namespace ProductManager.Interfaces
{
    public interface IProductService
    {
        void UpdateState(ProductDto productDto);
    }
}