using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductManager
{
    public class ProductItem
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Number { get; set; }

        [BsonElement("Qty")]
        public int Quantity { get; set; }

        [BsonElement("MinQty")]
        public int MinQuantity { get; set; }

        public Status State { get; set; }

        public int Category { get; set; }
    }

    public enum Status
    {
        Enough,
        Min,
        Finish
    }
}
