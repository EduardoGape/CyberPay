using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface ICompanyService
    {
        Company GetById(string id);
        IEnumerable<Company> GetAll(CompanyFilter Filter);
        string Register(Company Company);
        string Update(string id, Company Company);
        string Delete(string id);
    }
}