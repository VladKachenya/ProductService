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
            var productItemBuilder = Builders<Product>.IndexKeys;
            var indexOption = new CreateIndexOptions(){Unique = true};
            var indexModel = new CreateIndexModel<Product>(productItemBuilder.Ascending(x => x.Number), indexOption);
            Products.Indexes.CreateOne(indexModel);
        }

        public IMongoCollection<Product> Products => _db.GetCollection<Product>("products");
    }
}