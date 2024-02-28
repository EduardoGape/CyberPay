using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface ICategoryService
    {
        Category GetById(string id);
        IEnumerable<Category> GetAll(CategoryFilter Filter);
        string Register(Category category);
        string Update(string id, Category category);
        string Delete(string id);
    }
}