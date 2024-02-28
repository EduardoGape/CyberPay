using Application.IServices;
using Domain.Entities;
using Domain.Enum;
using Domain.Filters;
using Firebase.Database;
using Hangfire;
using Hangfire.Mongo;
using Infrastructure.Messages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IMongoCollection<Chat> _ChatsCollection;
        private readonly FirebaseClient _firebaseClient;
        public ChatService(IMongoCollection<Chat> ChatsCollection, FirebaseClient firebaseClient)
        {
            _ChatsCollection = ChatsCollection;
            _firebaseClient = firebaseClient;
        }

        public string Register(Chat Chat)
        {
            try
            {
                Chat = Util.FillEntity(Chat);
                _ChatsCollection.InsertOne(Chat);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Chat Chat)
        {
            try
            {
                var existingChat = _ChatsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingChat != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Chat>>();

                
                    var update = Builders<Chat>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Chat>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _ChatsCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.ChatNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Chat GetById(string id)
        {
            return _ChatsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Chat> GetAll(ChatFilter Filter)
        {
            return _ChatsCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _ChatsCollection.DeleteOne(p => p.Id.ToString() == id);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }

    }
}
