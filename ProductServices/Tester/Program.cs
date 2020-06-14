using System;
using System.Linq;
using System.Text;
using ProductService.DataTransfer;
using ProductService.DataTransfer.Client;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitProductChangeChannel client = new RabbitProductChangeChannel();
            var filter = new ProductChangesFilter();
            filter.QtyChanges = ChangeType.Dec | ChangeType.Inc | ChangeType.NoChange;
            filter.StateChanges = ChangeType.Dec | ChangeType.Inc | ChangeType.NoChange;
            filter.ProductNumbers.AddRange(new []{ 1, 2, 3, 4, 5, 6 });

            client.Subscribe(filter, (ch) => Console.WriteLine($"[x] Received {ch}, StateCh:{ch.GetStateChangeType()} QtyCh:{ch.GetQtyChangeType()}"));

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
