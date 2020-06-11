using System;

namespace ProductService.DataTransferObjects
{
    public class ProductItemDto
    {
        public int Number { get; set; }

        public int Quantity { get; set; }

        public int MinQuantity { get; set; }

        public int State { get; set; }

        public int Category { get; set; }
    }
}
