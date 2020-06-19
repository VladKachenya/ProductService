using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Data;
using ProductManager.Interfaces;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly DataMapper _dataMapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductChangesPublisher _productChangesPublisher;

        public ProductsController(
            IProductService productService,
            ILogger<ProductsController> logger,
            DataMapper dataMapper,
            IProductRepository productRepository,
            IProductChangesPublisher productChangesPublisher)
        {
            _productService = productService;
            _logger = logger;
            _dataMapper = dataMapper;
            _productRepository = productRepository;
            _productChangesPublisher = productChangesPublisher;
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int number)
        {
            try
            {
                if (number < 0)
                {
                    return BadRequest(nameof(number));
                }
                var product = await _productRepository.GetAsync(number);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(_dataMapper.ToProductDto(product));
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), exception: e, "LogError {0}", Request.Path);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                return Ok((await _productRepository.ListAllAsync()).Select(pi => _dataMapper.ToProductDto(pi)).ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), exception: e, "LogError {0}", Request.Path);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto productDto)
        {
            try
            {
                _productService.UpdateState(productDto);
                var prevProduct = await _productRepository.GetAsync(productDto.Number);
                if (prevProduct == null)
                {
                    return NotFound();
                }
                var prevProductDto = _dataMapper.ToProductDto(prevProduct);
                await _productRepository.UpdateAsync(_dataMapper.ToProduct(productDto));
                _productChangesPublisher.Publish(prevProductDto, productDto);
                return Ok(productDto);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), exception: e, "LogError {0}", Request.Path);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
        {
            try
            {
                _productService.UpdateState(product);
                await _productRepository.AddAsync(_dataMapper.ToProduct(product));
                Response.StatusCode = (int) HttpStatusCode.Created;
                return product;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), exception: e, "LogError {0}", Request.Path);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteProduct(int number)
        {
            try
            {
                if (number < 0)
                {
                    return BadRequest(nameof(number));
                }
                await _productRepository.DeleteAsync(number);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), exception: e, "LogError {0}", Request.Path);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
