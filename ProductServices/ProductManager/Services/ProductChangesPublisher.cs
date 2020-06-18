using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProductManager.Data;
using ProductService.DataTransfer.Client;
using ProductService.DataTransfer.Client.Factories;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;

namespace ProductManager.Services
{
    public class ProductChangesPublisher : IProductChangesPublisher
    {
        private readonly IChannelFactory _channelFactory;

        private Func<ProductDto, ProductDto, ProductChanges> _productChangeFactory => (p, c) => new ProductChanges
        {
            Number = c.Number,
            State = c.State,
            Qty = c.Qty,
            PrevQty = p.Qty,
            PrevState = p.State,
            Category = c.Category
        };

        public ProductChangesPublisher(IChannelFactory channelFactory, IConfiguration configuration)
        {
            _channelFactory = channelFactory;
        }

        public async void Publish(ProductDto preview, ProductDto current)
        {
            try
            {
                var productChange = _productChangeFactory(preview, current);
                await Task.Run(() => _channelFactory.CreatePublisher().Publish(productChange));
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}