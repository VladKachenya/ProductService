using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProductService.DataTransfer.Client;
using ProductServices.Notifier.Hubs;

namespace ProductServices.Notifier.System
{
    public class NotificationHelper : INotificationHelper
    {
        private readonly IHubContext<ProductChangesHub> _context;

        public NotificationHelper(IHubContext<ProductChangesHub> context)
        {
            _context = context;
        }
        public void SetProductChangesNotification(string connectionId, IListener listener)
        {
            if(listener == null) throw new ArgumentNullException(nameof(listener));
            if (string.IsNullOrWhiteSpace(connectionId)) throw new ArgumentNullException(nameof(connectionId));

            try
            {
                listener.Subscribe(async change =>
                    {
                        await _context.Clients.Clients(connectionId).SendAsync("product_changes", change);
                    });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
