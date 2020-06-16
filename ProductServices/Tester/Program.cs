using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ProductService.DataTransfer.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Tester
{
    class Program
    {

        public class ProductChangesDot
        {
            public IEnumerable<string> QtyCh { get; set; }
            public IEnumerable<string> StateCh { get; set; }
            public IEnumerable<int> Categories { get; set; }
        }

        private static HubConnection _connection;
        static void Main(string[] args)
        {
            _connection = new HubConnectionBuilder()
                //.WithUrl("http://localhost:5000/product_changes")
                .WithUrl("https://localhost:44366/product_changes")
                .Build();

            var mes = new ProductChangesDot() { Categories = new List<int>() { 1, 2, 3 }, QtyCh = new List<string>() { "asdf", "sadf" }, StateCh = new List<string>() { "asdf" } };
            var json = JsonSerializer.Serialize(mes);

           
            Run();
            //BaseRabbitChannel client = new BaseRabbitChannel();
            //var filter = new ProductChangesFilter();
            //filter.QtyChanges = ChangeType.Dec | ChangeType.Inc | ChangeType.NoChange;
            //filter.StateChanges = ChangeType.Dec | ChangeType.Inc | ChangeType.NoChange;
            //filter.ProductCategories.AddRange(new []{ 1, 2, 3, 4, 5, 6 });

            //client.Subscribe(filter, (ch) => Console.WriteLine($"[x] Received {ch}, StateCh:{ch.GetStateChangeType()} QtyCh:{ch.GetQtyChangeType()}"));

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static async void Run()
        {
            _connection.On<ProductChange>("product_changes", (user) => Console.WriteLine($"[x] Received {user}"));

            await _connection.StartAsync();

            var mes = new ProductChangesDot();//{Categories = new List<int>(){1, 2, 3, 4, 5, 6, 7}, QtyCh = new List<string>(){ "Inc", "NoChange" }, StateCh = new List<string>(){ "Dec" } };
            var json = JsonSerializer.Serialize(mes);

            await _connection.InvokeAsync("SetFilter", mes);

            //await Task.Delay(2000);

            //await _connection.InvokeAsync("SetFilter", "Message");
        }
    }
}
