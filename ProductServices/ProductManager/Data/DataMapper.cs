using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.DataTransferObjects;

namespace ProductManager.Data
{
    public class DataMapper
    {
        public ProductItem ToProductItem(ProductItemDto productItemDto)
        {
            return new ProductItem
            {
                Number = productItemDto.Number,
                MinQuantity = productItemDto.MinQuantity,
                Category = productItemDto.Category,
                State = (Status)productItemDto.State,
                Quantity = productItemDto.Quantity
            };
        }

        public ProductItemDto ToProductItemDto(ProductItem productItem)
        {
            return new ProductItemDto
            {
                Number = productItem.Number,
                MinQuantity = productItem.MinQuantity,
                Category = productItem.Category,
                State = (int)productItem.State,
                Quantity = productItem.Quantity
            };
        }
    }
}
