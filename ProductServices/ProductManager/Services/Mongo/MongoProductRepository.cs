using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductManager.Data;
using ProductManager.Interfaces;

namespace ProductManager.Services.Mongo
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly MongoDbContext _dbContext;


        public MongoProductRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Product> GetAsync(int productNumber)
        {
           return await _dbContext.Products.Find(GetNumberFilter(productNumber))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> ListAllAsync()
        {
            var res = await _dbContext.Products.Find(new FilterDefinitionBuilder<Product>().Empty).ToListAsync();
            return res;
        }

        public async Task<Product> AddAsync(Product product)
        {
            if(product == null) throw new ArgumentNullException(nameof(product));

            await _dbContext.Products.InsertOneAsync(product);
            
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            await _dbContext.Products.ReplaceOneAsync(GetNumberFilter(product.Number), product);
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