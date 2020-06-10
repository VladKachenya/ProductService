using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IProductChangesPublisher
    {
        Task Publish(ProductItem preview, ProductItem current);
    }
}