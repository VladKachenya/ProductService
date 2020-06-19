using ProductService.DataTransfer.Channel;

namespace ProductServices.Notifier.Interfaces
{
    public interface INotificationHelper
    {
        void SetProductChangesNotification(string connectionId, IListener listener);
    }
}
