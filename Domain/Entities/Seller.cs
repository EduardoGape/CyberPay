using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Seller : BaseData
    {
        [BsonElement("fullName")]
        public string? FullName { get; set; }
        [BsonElement("email")]
        public string? Email { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
        [BsonElement("dataBirth")]
        public long? DateBirth { get; set; }
        [BsonElement("typeProfile")]
        public TypeProfile TypeProfile {get;set;}
        [BsonElement("listClients")]
        public List<ProfileAux>? ListClient {get; set;}
        [BsonElement("called")]
        public List<Chat>? Called {get; set;}
    }
}