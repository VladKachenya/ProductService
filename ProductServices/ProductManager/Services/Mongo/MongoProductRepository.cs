using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductManager.Data;

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
           return await _dbContext.Products.Find(GetNumberFilter(productNumber))
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductItem>> ListAllAsync()
        {
            var res = await _dbContext.Products.Find(new FilterDefinitionBuilder<ProductItem>().Empty).ToListAsync();
            return res;
        }

        public async Task<ProductItem> AddAsync(ProductItem productItem)
        {
            if(productItem == null) throw new ArgumentNullException(nameof(productItem));

            await _dbContext.Products.InsertOneAsync(productItem);
            
            return productItem;
        }

        public async Task UpdateAsync(ProductItem productItem)
        {
            if (productItem == null) throw new ArgumentNullException(nameof(productItem));

            await _dbContext.Products.ReplaceOneAsync(GetNumberFilter(productItem.Number), productItem);
        }

        public async Task DeleteAsync(int productNumber)
        {
            await _dbContext.Products.DeleteOneAsync(GetNumberFilter(productNumber));
        }

        private BsonDocument GetNumberFilter(int productNumber)
        {
            return new BsonDocument(Constants.Product.Number, productNumber);
        }
    }
}