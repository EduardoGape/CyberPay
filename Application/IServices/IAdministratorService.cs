using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IAdministratorService
    {
        Administrator GetById(string id);
        IEnumerable<Administrator> GetAll(AdministratorFilter Filter);
        string Register(Administrator administrator);
        string Update(string id, Administrator administrator);
        string Delete(string id);
    }
}