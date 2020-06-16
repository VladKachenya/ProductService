using System;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;

namespace ProductService.DataTransfer.Client
{
    internal class ProductChangePublisher : BaseProductChangeChannel, IPublisher
    {

        public ProductChangePublisher(string exchangeName = null, string rabbitConnectionString = null)
            :base(exchangeName, rabbitConnectionString)
        {
        }
        public void Publish(ProductChange productChange)
        {
            if (productChange == null) throw new ArgumentNullException(nameof(productChange));


                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();

                try
                {
                    channel.ExchangeDeclare(_exchangeName, _exchangeType);
                    var routingKey = _routingKeyFactory.GetPublicationKey(productChange);
                    channel.BasicPublish(
                        exchange: _exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: _dataSerializer.ToBson(productChange));
                }
                finally
                {
                    connection.Close();
                    channel.Close();
                }
        }
    }
}