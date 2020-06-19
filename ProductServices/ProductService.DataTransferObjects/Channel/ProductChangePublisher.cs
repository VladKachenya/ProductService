using System;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;

namespace ProductService.DataTransfer.Channel
{
    internal class ProductChangePublisher : BaseProductChangeChannel, IPublisher
    {

        public ProductChangePublisher(string exchangeName = null, string rabbitConnectionString = null)
            :base(exchangeName, rabbitConnectionString)
        {
        }
        public void Publish(ProductChanges productChanges)
        {
            if (productChanges == null) throw new ArgumentNullException(nameof(productChanges));


                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();

                try
                {
                    channel.ExchangeDeclare(_exchangeName, _exchangeType);
                    var routingKey = _routingKeyFactory.GetPublicationKey(productChanges);
                    channel.BasicPublish(
                        exchange: _exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: _dataSerializer.ToBson(productChanges));
                }
                finally
                {
                    connection.Close();
                    channel.Close();
                }
        }
    }
}