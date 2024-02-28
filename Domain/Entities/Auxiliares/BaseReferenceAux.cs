using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class BaseReferenceAux 
    {
        [BsonElement("Id")]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
    }
}