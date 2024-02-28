using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IMatterService
    {
        Matter GetById(string id);
        IEnumerable<Matter> GetAll(MatterFilter Filter);
        string Register(Matter Matter);
        string Update(string id, Matter Matter);
        string Delete(string id);
    }
}