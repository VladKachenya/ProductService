using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.DataTransfer.Client;

namespace ProductServices.Notifier.System
{
    public interface IListenerManager
    {
        void Create(string connectionId);

        void Remove(string connectionId);

        IListener GetListener(string connectionId);
    }
}
