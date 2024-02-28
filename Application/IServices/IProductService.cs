using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IProductService
    {
        Product GetById(string id);
        IEnumerable<Product> GetAll(ProductFilter Filter);
        string Register(Product Product);
        string Update(string id, Product Product);
        string Delete(string id);
    }
}