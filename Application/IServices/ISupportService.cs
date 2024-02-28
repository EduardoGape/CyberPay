using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface ISupportService
    {
        Support GetById(string id);
        IEnumerable<Support> GetAll(SupportFilter Filter);
        string Register(Support Support);
        string Update(string id, Support Support);
        string Delete(string id);
    }
}