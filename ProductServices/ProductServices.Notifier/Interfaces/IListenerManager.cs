using ProductService.DataTransfer.Channel;

namespace ProductServices.Notifier.Interfaces
{
    public interface IListenerManager
    {
        void Create(string connectionId);

        void Remove(string connectionId);

        IListener GetListener(string connectionId);
    }
}
