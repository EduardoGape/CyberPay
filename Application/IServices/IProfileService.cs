using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IProfileService
    {
        Profile GetById(string id);
        IEnumerable<Profile> GetAll(ProfileFilter Filter);
        string Register(Profile profile);
        string Update(string id, Profile profile);
        string Delete(string id);
    }
}