using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProductService.DataTransfer.Client;
using ProductServices.Notifier.Data;
using ProductServices.Notifier.Hubs;

namespace ProductServices.Notifier.System
{
    public class NotificationHelper : INotificationHelper
    {
        private readonly IHubContext<ProductChangesHub> _context;
        private readonly DataMapper _dataMapper;

        public NotificationHelper(
            IHubContext<ProductChangesHub> context,
            DataMapper dataMapper)
        {
            _context = context;
            _dataMapper = dataMapper;
        }
        public void SetProductChangesNotification(string connectionId, IListener listener)
        {
            if(listener == null) throw new ArgumentNullException(nameof(listener));
            if (string.IsNullOrWhiteSpace(connectionId)) throw new ArgumentNullException(nameof(connectionId));

            try
            {
                listener.Subscribe(async changes =>
                    {
                        await _context.Clients.Clients(connectionId).SendAsync("product_changes", _dataMapper.ToProductChangesDto(changes));
                    });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
