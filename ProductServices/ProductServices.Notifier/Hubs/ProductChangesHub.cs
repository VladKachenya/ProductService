using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProductServices.Notifier.Data;
using ProductServices.Notifier.Interfaces;
using ProductServices.Notifier.System;

namespace ProductServices.Notifier.Hubs
{
    public class ProductChangesHub : Hub
    {
        private readonly IListenerManager _listenerManager;
        private readonly INotificationHelper _notificationHelper;
        private readonly DataMapper _dataMapper;

        public ProductChangesHub(
            IListenerManager listenerManager,
            INotificationHelper notificationHelper,
            DataMapper dataMapper)
        {
            _listenerManager = listenerManager;
            _notificationHelper = notificationHelper;
            _dataMapper = dataMapper;
        }

        [HubMethodName("set_filter")]
        public void SetFilter(ProductChangesFilterDto filterMassage)
        {
            var filterModel = _dataMapper.ToProductChangesFilter(filterMassage);
            var listener = _listenerManager.GetListener(this.Context.ConnectionId);
            listener.Configure(filterModel);
            _notificationHelper.SetProductChangesNotification(this.Context.ConnectionId, listener);
        }


        public override Task OnConnectedAsync()
        {
            _listenerManager.Create(this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _listenerManager.Remove(this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}