using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Filters
{
    public class ListActivityFilter 
    {
        public string? Name {get;set;}
    }
}
