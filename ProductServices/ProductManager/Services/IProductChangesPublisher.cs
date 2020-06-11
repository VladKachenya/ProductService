using System.Threading.Tasks;
using ProductManager.Data;

namespace ProductManager.Services
{
    public interface IProductChangesPublisher
    {
        Task Publish(ProductItem preview, ProductItem current);
    }
}