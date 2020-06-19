using System;
using System.Threading.Tasks;
using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Channel
{
    public interface IListener
    {
        void Configure(ProductChangesFilter changesFilter);
        void Subscribe(Func<ProductChanges, Task> action);
        void Close();
    }
}