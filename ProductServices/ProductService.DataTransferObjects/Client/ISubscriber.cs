using System;
using ProductService.DataTransfer.Data;

namespace ProductService.DataTransfer.Client
{
    public interface ISubscriber
    {
        void Subscribe(ProductChangesFilter changesFilter, Action<ProductChange> action);
    }
}