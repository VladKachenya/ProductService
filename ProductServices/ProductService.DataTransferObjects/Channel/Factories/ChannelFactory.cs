using ProductService.DataTransfer.Data;
using RabbitMQ.Client;

namespace ProductService.DataTransfer.Channel.Factories
{
    public class ChannelFactory : IChannelFactory
    {
        public string ChannelName { get; set; }
        public string ConnectionString { get; set; }

        public IListener CreateListener()
        {
            return new ProductChangeListener(ChannelName, ConnectionString);
        }

        public IPublisher CreatePublisher()
        {
            return new ProductChangePublisher(ChannelName, ConnectionString);
        }
    }
}