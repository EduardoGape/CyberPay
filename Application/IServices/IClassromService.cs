using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IClassromService
    {
        Classrom GetById(string id);
        IEnumerable<Classrom> GetAll(ClassromFilter Filter);
        string Register(Classrom Classrom);
        string Update(string id, Classrom Classrom);
        string Delete(string id);
    }
}