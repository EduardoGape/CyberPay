using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ListActivityAux 
    {
        [BsonElement("question")]
        public string? Question { get; set; }
        [BsonElement("Description")]
        public string? Description { get; set; }
        [BsonElement("option")]
        public List<OptionAux>? Option { get; set; }
    }
}