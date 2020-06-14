using System;
using System.Threading.Tasks;
using ProductManager.Data;
using ProductService.DataTransfer.Client;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;

namespace ProductManager.Services
{
    public class ProductChangesPublisher : IProductChangesPublisher
    {
        private IPublisher _publisher;

        private Func<ProductDto, ProductDto, ProductChange> _productChangeFactory => (p, c) => new ProductChange
        {
            Number = c.Number,
            State = c.State,
            Qty = c.Qty,
            PrevQty = p.Qty,
            PrevState = p.State
        };

        public ProductChangesPublisher()
        {
            _publisher = new RabbitProductChangeChannel();
        }

        public async void Publish(ProductDto preview, ProductDto current)
        {
            try
            {
                var productChange = _productChangeFactory(preview, current);
                await Task.Run(() => _publisher.Publish(productChange));
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}