using ProductManager.Data;

namespace ProductManager.Interfaces
{
    public interface IProductChangesPublisher
    {
        void Publish(ProductDto preview, ProductDto current);
    }
}