using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductManager.Data;
using ProductManager.Services;
using ProductService.DataTransferObjects;

namespace ProductManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DataMapper _dataMapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            ILogger<ProductsController> logger,
            DataMapper dataMapper,
            IProductRepository productRepository)
        {
            _logger = logger;
            _dataMapper = dataMapper;
            _productRepository = productRepository;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ProductItemDto>> GetProductItem(int number)
        {
            return _dataMapper.ToProductItemDto(await _productRepository.GetAsync(number));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetProducts()
        {
            return (await _productRepository.ListAllAsync()).Select(pi => _dataMapper.ToProductItemDto(pi)).ToList();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductItem(ProductItemDto productItem)
        {
            await _productRepository.UpdateAsync(_dataMapper.ToProductItem(productItem));
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductItemDto>> CreateProductItem(ProductItemDto productItem)
        {
            await _productRepository.AddAsync(_dataMapper.ToProductItem(productItem));
            return productItem;
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteProductItem(int number)
        {
            await _productRepository.DeleteAsync(number);
            return NoContent();
        }
    }
}
