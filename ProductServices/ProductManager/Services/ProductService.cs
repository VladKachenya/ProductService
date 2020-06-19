using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Data;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        public void UpdateState(ProductDto productDto)
        {
            if (productDto == null) throw new ArgumentNullException(nameof(productDto));

            var middleQty = productDto.MinQty * 1.2;
            if (productDto.Qty > middleQty)
            {
                productDto.State = 0;
            }
            else if (productDto.Qty < middleQty && productDto.Qty > productDto.MinQty)
            {
                productDto.State = 1;
            }
            else
            {
                productDto.State = 2;
            }
        }
    }
}
