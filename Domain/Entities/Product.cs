using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Product : BaseData
    {
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("value")]
        public double? Value { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("seller")]
        public ProfileAux? Seller { get; set; }
        [BsonElement("company")]
        public CompanyAux? Company { get; set; }
        [BsonElement("assessments")]
        public List<ProfileAux>? Assessments { get; set; }
        [BsonElement("image")]
        public string? Image {get; set;}

    }
}