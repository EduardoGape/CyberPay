using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IFinancialHistoryService
    {
        FinancialHistory GetById(string id);
        IEnumerable<FinancialHistory> GetAll(FinancialHistoryFilter Filter);
        string Register(FinancialHistory FinancialHistory);
        string Update(string id, FinancialHistory FinancialHistory);
        string Delete(string id);
    }
}