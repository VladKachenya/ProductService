using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProductService.DataTransfer.Channel;
using ProductServices.Notifier.Data;
using ProductServices.Notifier.Hubs;
using ProductServices.Notifier.Interfaces;

namespace ProductServices.Notifier.System
{
    public class NotificationHelper : INotificationHelper
    {
        private readonly ILogger _logger;
        private readonly IHubContext<ProductChangesHub> _context;
        private readonly DataMapper _dataMapper;

        public NotificationHelper(
            ILogger<NotificationHelper> logger,
            IHubContext<ProductChangesHub> context,
            DataMapper dataMapper)
        {
            _logger = logger;
            _context = context;
            _dataMapper = dataMapper;
        }
        public void SetProductChangesNotification(string connectionId, IListener listener)
        {
            try
            {
                if (listener == null) throw new ArgumentNullException(nameof(listener));
                if (string.IsNullOrWhiteSpace(connectionId)) throw new ArgumentNullException(nameof(connectionId));


                listener.Subscribe(async changes =>
                    {
                        try
                        {
                            await _context.Clients.Clients(connectionId).SendAsync(Constants.product_changes, _dataMapper.ToProductChangesDto(changes));
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, $"Sending changes: {changes} to client {connectionId}");
                        }
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
