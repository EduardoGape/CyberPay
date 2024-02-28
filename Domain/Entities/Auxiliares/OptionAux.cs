using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class OptionAux 
    {
        [BsonElement("letter")]
        public string? Letter { get; set; }
        [BsonElement("value")]
        public string? Value { get; set; }
    }
}