using System.Threading.Tasks;
using ProductManager.Data;

namespace ProductManager.Services
{
    public interface IProductChangesPublisher
    {
        void Publish(ProductDto preview, ProductDto current);
    }
}