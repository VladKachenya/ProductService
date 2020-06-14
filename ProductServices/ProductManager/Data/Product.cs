using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductManager.Data
{
    public class Product
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public int Number { get; set; }

        public int Qty { get; set; }

        public int MinQty { get; set; }

        public Status State { get; set; }

        public int Category { get; set; }
    }

    public enum Status
    {
        Many,
        Middle,
        Min
    }
}
