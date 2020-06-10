using System;

namespace ProductManager
{
    public class ProductItem
    {
        public int Number { get; set; }

        public int Qty { get; set; }
        public int MinQty { get; set; }
        public Status State { get; set; }
        public int Category { get; set; }
    }

    public enum Status
    {
        Sufficient,
        Min,
        Finish
    }
}
