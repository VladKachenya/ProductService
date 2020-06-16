using System;
using System.Linq;
using System.Text;
using ProductService.DataTransfer.Client.Factories;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.DataTransfer.Client
{
    internal abstract class BaseProductChangeChannel 
    {
        protected readonly string _exchangeName;
        protected readonly ConnectionFactory _connectionFactory;
        protected readonly string _exchangeType = ExchangeType.Topic;
        protected readonly DataSerializer<ProductChange> _dataSerializer;
        protected readonly RoutingKeyFactory _routingKeyFactory;


        protected BaseProductChangeChannel(string exchangeName = null, string rabbitConnectionString = null)
        {
            _exchangeName = exchangeName ?? Constants.DefaultExchangeName;
            _connectionFactory = new ConnectionFactory();
            if (!string.IsNullOrWhiteSpace(rabbitConnectionString))
            {
                _connectionFactory.Uri = new Uri(rabbitConnectionString);
            }
            _routingKeyFactory = new RoutingKeyFactory();
            _dataSerializer = new DataSerializer<ProductChange>();
        }
    }
}