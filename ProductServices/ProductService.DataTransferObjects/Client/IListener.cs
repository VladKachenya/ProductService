using System;
using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Client
{
    public interface IListener
    {
        void Configure(ProductChangesFilter changesFilter);
        void Subscribe(Action<ProductChanges> action);
        void Close();
    }
}