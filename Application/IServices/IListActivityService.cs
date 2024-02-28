using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IListActivityService
    {
        ListActivity GetById(string id);
        IEnumerable<ListActivity> GetAll(ListActivityFilter Filter);
        string Register(ListActivity ListActivity);
        string Update(string id, ListActivity ListActivity);
        string Delete(string id);
    }
}