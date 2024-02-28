using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Filters
{
    public class ProfileFilter 
    {
        public TypeProfile? TypeProfile {get;set;}
    }
}
