using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(int number)
        {
            await Task.Delay(10);
            return new ProductItem() { Category = 1, MinQuantity = 2, Number = 3 };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts()
        {
            return await _productRepository.ListAllAsync();
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
            await _productRepository.AddAsync(productItem);
            return new ProductItem() { Category = 1, MinQuantity = 2, Number = 3 };
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteProductItem(int number)
        {
            await Task.Delay(10);
            return NoContent();
        }
    }
}
