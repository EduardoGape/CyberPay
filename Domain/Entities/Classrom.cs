using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Classrom : BaseData
    {
        [BsonElement("listStudents")]
        public List<ProfileAux>? ListStudents {get; set;}
        [BsonElement("teacher")]
        public ProfileAux? Teacher {get; set;}
        [BsonElement("category")]
        public BaseReferenceAux? Category { get; set; }
        [BsonElement("matter")]
        public BaseReferenceAux? Matter { get; set; }
        [BsonElement("listActivities")]
        public List<ListActivity>? ListActivities {get; set;}
    }
}