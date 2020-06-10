using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(int number)
        {
            await Task.Delay(10);
            return new ProductItem() { Category = 1, MinQty = 2, Number = 3 };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts()
        {
            await Task.Delay(10);
            return new List<ProductItem>
            {
                new ProductItem() {Category = 1, MinQty = 2, Number = 3},
                new ProductItem() {Category = 2, MinQty = 2, Number = 3}
            };
        }

        [HttpPut("{number}")]
        public async Task<ActionResult> UpdateProductItem(ProductItem productItem)
        {
            await Task.Delay(10);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> CreateProductItem(ProductItem productItem)
        {
            await Task.Delay(10);
            return new ProductItem() { Category = 1, MinQty = 2, Number = 3 };
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteProductItem(int number)
        {
            await Task.Delay(10);
            return NoContent();
        }
    }
}
