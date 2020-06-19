using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.DataTransfer.Channel;

namespace ProductServices.Notifier.System
{
    public interface INotificationHelper
    {
        void SetProductChangesNotification(string connectionId, IListener listener);
    }
}
