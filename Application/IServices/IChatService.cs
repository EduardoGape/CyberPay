using Domain.Entities;
using Domain.Filters;

namespace Application.IServices
{
    public interface IChatService
    {
        Chat GetById(string id);
        IEnumerable<Chat> GetAll(ChatFilter Filter);
        string Register(Chat chat);
        string Update(string id, Chat chat);
        string Delete(string id);
    }
}