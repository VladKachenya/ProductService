using System;
using ProductManager.Data;
using ProductManager.Services;

namespace TestPublishing
{
    class Program
    {
        static void Main(string[] args)
        {
            var pub = new ProductChangesPublisher();
            var prod1 = new ProductDto() {Number = 1, Category = 1, State = 2, MinQty = 4, Qty = 9};
            var prod2 = new ProductDto() {Number = 1, Category = 1, State = 2, MinQty = 4, Qty = 6};
            pub.Publish(prod1, prod2);
            Console.ReadLine();
        }
    }
}
