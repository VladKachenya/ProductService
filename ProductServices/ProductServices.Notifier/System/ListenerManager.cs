using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ProductService.DataTransfer.Client;
using ProductService.DataTransfer.Client.Factories;
using ProductServices.Notifier.Hubs;

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
