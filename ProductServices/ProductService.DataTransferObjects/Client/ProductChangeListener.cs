using System;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.DataTransfer.Client
{
    internal class ProductChangeListener : BaseProductChangeChannel, IListener
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public ProductChangeListener(string exchangeName = null, string rabbitConnectionString = null)
            : base(exchangeName, rabbitConnectionString)
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, _exchangeType);
        }

        public void Configure(ProductChangesFilter changesFilter)
        {
            if (changesFilter == null) throw new ArgumentNullException(nameof(changesFilter));
            if (_queueName != null) _channel.QueueDelete(_queueName);
            _queueName = _channel.QueueDeclare().QueueName;

            var routingKeys = _routingKeyFactory.GetBindingKeys(changesFilter);

            foreach (var routingKey in routingKeys)
            {
                _channel.QueueBind(queue: _queueName,
                    exchange: _exchangeName,
                    routingKey: routingKey);
            }
        }

        public void Subscribe(Action<ProductChanges> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (_queueName == null) throw new InvalidOperationException("The channel was not configured");


            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var changes = _dataSerializer.FromBson(body);
                action(changes);
            };

            _channel.BasicConsume(
                queue: _queueName,
                autoAck: true,
                consumer: consumer);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}