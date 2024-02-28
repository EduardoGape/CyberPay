using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface ISellerService
    {
        Seller GetById(string id);
        IEnumerable<Seller> GetAll(SellerFilter Filter);
        string Register(Seller Seller);
        string Update(string id, Seller Seller);
        string Delete(string id);
    }
}