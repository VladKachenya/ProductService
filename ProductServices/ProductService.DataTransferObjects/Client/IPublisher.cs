using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Client
{
    public interface IPublisher
    {
        void Publish(ProductChange productChange);
    }
}