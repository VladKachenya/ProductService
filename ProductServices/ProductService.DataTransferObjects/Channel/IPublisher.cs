using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Channel
{
    public interface IPublisher
    {
        void Publish(ProductChanges productChanges);
    }
}