using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace ProductService.DataTransfer.Data
{
    internal class DataSerializer<T> where T : class
    {

        public byte[] ToBson(T item)
        {
            using var ms = new MemoryStream();
            using var dataWriter = new BsonDataWriter(ms);
            var serializer = new JsonSerializer();
            serializer.Serialize(dataWriter, item);

            return ms.ToArray();
        }

        public T FromBson(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var reader = new BsonDataReader(ms);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }
    }
}