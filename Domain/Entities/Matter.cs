using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Matter : BaseData
    {
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
    }
}