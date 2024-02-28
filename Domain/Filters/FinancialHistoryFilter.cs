using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Filters
{
    public class FinancialHistoryFilter 
    {
        public string? Name {get;set;}
    }
}
