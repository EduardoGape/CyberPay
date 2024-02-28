using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ChatAux 
    {
        [BsonElement("peopleOne")]
        public ProfileAux? PeopleOne {get; set;}
        [BsonElement("peopleTwo")]
        public ProfileAux? PeopleTwo {get; set;}
        [BsonElement("people")]
        public List<ProfileAux>? People {get; set;}
        [BsonElement("typeChat")]
        public TypeChat TypeChat {get; set;}
    }
}