using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class BaseData
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("dataBloced")]
        public long? DateBlocked { get; set; }
        [BsonElement("created")]
        public long? Created { get; set; }
        [BsonElement("updated")]
        public long? Updated {get; set;}

    }
}