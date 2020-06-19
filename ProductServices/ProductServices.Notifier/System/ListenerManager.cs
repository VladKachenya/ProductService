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
        private static readonly Dictionary<string, IListener> Listeners = new Dictionary<string, IListener>();

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
            lock (Listeners)
            {
                if (!Listeners.ContainsKey(connectionId))
                {
                    Listeners.Add(connectionId, _channelFactory.CreateListener());
                }
            }
        }

        public void Remove(string connectionId)
        {
            lock (Listeners)
            {
                if (Listeners.ContainsKey(connectionId))
                {
                    Listeners[connectionId].Close();
                    Listeners.Remove(connectionId);
                }
            }
        }

        public IListener GetListener(string connectionId)
        {
            lock (Listeners)
            {
                if (Listeners.ContainsKey(connectionId))
                {
                    return Listeners[connectionId];
                }
                return null;
            }
        }
    }
}
