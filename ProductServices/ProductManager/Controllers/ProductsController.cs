using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Data;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DataMapper _dataMapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductChangesPublisher _productChangesPublisher;

        public ProductsController(
            ILogger<ProductsController> logger,
            DataMapper dataMapper,
            IProductRepository productRepository,
            IProductChangesPublisher productChangesPublisher)
        {
            _logger = logger;
            _dataMapper = dataMapper;
            _productRepository = productRepository;
            _productChangesPublisher = productChangesPublisher;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int number)
        {
            return _dataMapper.ToProductDto(await _productRepository.GetAsync(number));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            //await _productChangesPublisher.Publish(null, null);
            return (await _productRepository.ListAllAsync()).Select(pi => _dataMapper.ToProductDto(pi)).ToList();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto product)
        {
            UpdateState(product);
            var prevProduct = _dataMapper.ToProductDto(await _productRepository.GetAsync(product.Number));
            await _productRepository.UpdateAsync(_dataMapper.ToProduct(product));
            _productChangesPublisher.Publish(prevProduct, product);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
        {
            UpdateState(product);
            await _productRepository.AddAsync(_dataMapper.ToProduct(product));
            return product;
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteProduct(int number)
        {
            await _productRepository.DeleteAsync(number);
            return NoContent();
        }

        //For this needs refactoring
        private void UpdateState(ProductDto productDto)
        {
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
