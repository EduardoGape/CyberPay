using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ListActivity : BaseData
    {
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("teacher")]
        public ProfileAux? Teacher { get; set; }
        [BsonElement("category")]
        public BaseReferenceAux? Category { get; set; }
        [BsonElement("matter")]
        public BaseReferenceAux? Matter { get; set; }
        
        [BsonElement("activitys")]
        public List<ListActivityAux>? Activitys {get;set;}
    }
}