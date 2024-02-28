using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Administrator : BaseData
    {
        [BsonElement("fullName")]
        public string? FullName { get; set; }
        [BsonElement("email")]
        public string? Email { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
        [BsonElement("typeProfile")]
        public TypeProfile TypeProfile {get;set;}

    }
}
