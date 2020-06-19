using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ProductService.DataTransfer.Channel;
using ProductService.DataTransfer.Channel.Factories;
using ProductServices.Notifier.Hubs;
using ProductServices.Notifier.Interfaces;

namespace ProductServices.Notifier.System
{
    public class ListenerManager : IListenerManager
    {
        private readonly IChannelFactory _channelFactory;
        private readonly IHubContext<ProductChangesHub> _context;
        private readonly Dictionary<string, IListener> _listeners = new Dictionary<string, IListener>();

        public ListenerManager(
            IChannelFactory channelFactory,
            IHubContext<ProductChangesHub> context,
            IConfiguration configuration)
        {
            _channelFactory = channelFactory;
            _channelFactory.ConnectionString = configuration.GetSection("RabbitConnectionStrings").GetSection(Constants.ConnectionString).Value;
            _channelFactory.ChannelName = configuration.GetSection("RabbitConnectionStrings").GetSection(Constants.ExchangeName).Value;
            _context = context;
        }

        public void Create(string connectionId)
        {
            lock (_listeners)
            {
                if (!_listeners.ContainsKey(connectionId))
                {
                    _listeners.Add(connectionId, _channelFactory.CreateListener());
                }
            }
        }

        public void Remove(string connectionId)
        {
            lock (_listeners)
            {
                if (_listeners.ContainsKey(connectionId))
                {
                    _listeners[connectionId].Close();
                    _listeners.Remove(connectionId);
                }
            }
        }

        public IListener GetListener(string connectionId)
        {
            lock (_listeners)
            {
                if (_listeners.ContainsKey(connectionId))
                {
                    return _listeners[connectionId];
                }
                return null;
            }
        }
    }
}
