using System;
using System.Linq;
using System.Text;
using ProductService.DataTransfer.Client.Factories;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.DataTransfer.Client
{
    public class RabbitProductChangeChannel : IPublisher, ISubscriber
    {
        private readonly string _exchangeName;
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _exchangeType = ExchangeType.Topic;
        private readonly DataSerializer<ProductChange> _dataSerializer;
        private readonly RoutingKeyFactory _routingKeyFactory;

        private object _publishSynch = new object();

        public RabbitProductChangeChannel(string exchangeName = null, string rabbitConnectionString = null)
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

        public void Publish(ProductChange productChange)
        {
            if (productChange == null) throw new ArgumentNullException(nameof(productChange));

            lock (_publishSynch)
            {
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
                    //connection.Close();
                    //channel.Close();
                }
            }
        }



        public void Subscribe(ProductChangesFilter changesFilter, Action<ProductChange> action)
        {
            if (changesFilter == null) throw new ArgumentNullException(nameof(changesFilter));
            if (action == null) throw new ArgumentNullException(nameof(action));


            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel(); 
            //using var connection = _connectionFactory.CreateConnection();
            //using var channel = connection.CreateModel();
            try
            {
                channel.ExchangeDeclare(_exchangeName, _exchangeType);
                var queueName = channel.QueueDeclare().QueueName;
                var routingKeys = _routingKeyFactory.GetBindingKeys(changesFilter);

                foreach (var routingKey in routingKeys)
                {
                    channel.QueueBind(queue: queueName,
                        exchange: _exchangeName,
                        routingKey: routingKey);
                }

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var changes = _dataSerializer.FromBson(body);
                    action(changes);
                };

                channel.BasicConsume(
                    queue: queueName,
                    autoAck: true,
                    consumer: consumer);
            }
            finally
            {
                //connection.Close();
                //channel.Close();
            }
        }
    }
}