using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProductManager.Data;

namespace ProductManager.Services.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection(Constants.MongoDb).Value;
            var connection = new MongoUrlBuilder(connectionString);
            _db = (new MongoClient(connectionString)).GetDatabase(connection.DatabaseName);

            // Create unique index
            var productItemBuilder = Builders<ProductItem>.IndexKeys;
            var indexOption = new CreateIndexOptions(){Unique = true};
            var indexModel = new CreateIndexModel<ProductItem>(productItemBuilder.Ascending(x => x.Number), indexOption);
            Products.Indexes.CreateOne(indexModel);
        }

        public IMongoCollection<ProductItem> Products => _db.GetCollection<ProductItem>("products");
    }
}