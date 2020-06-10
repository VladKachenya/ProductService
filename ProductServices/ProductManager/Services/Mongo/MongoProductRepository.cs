using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ProductManager.Services.Mongo
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly MongoDbContext _dbContext;


        public MongoProductRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ProductItem> GetAsync(int productNumber)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductItem>> ListAllAsync()
        {
            var res = await _dbContext.Products.Find(new FilterDefinitionBuilder<ProductItem>().Empty).ToListAsync();
            return res;
        }

        public async Task<ProductItem> AddAsync(ProductItem productItem)
        {
            await _dbContext.Products.InsertOneAsync(productItem);
            return productItem;
        }

        public async Task UpdateAsync(ProductItem productItem)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(ProductItem productItem)
        {
            throw new System.NotImplementedException();
        }
    }
}