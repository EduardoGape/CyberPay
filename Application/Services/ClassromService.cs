using Application.IServices;
using Domain.Entities;
using Domain.Enum;
using Domain.Filters;
using Hangfire;
using Hangfire.Mongo;
using Infrastructure.Messages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Services
{
    public class ClassromService : IClassromService
    {
        private readonly IMongoCollection<Classrom> _ClassromsCollection;

        public ClassromService(IMongoCollection<Classrom> ClassromsCollection)
        {
            _ClassromsCollection = ClassromsCollection;
        }

        public string Register(Classrom Classrom)
        {
            try
            {
                Classrom = Util.FillEntity(Classrom);
                _ClassromsCollection.InsertOne(Classrom);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Classrom Classrom)
        {
            try
            {
                var existingClassrom = _ClassromsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingClassrom != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Classrom>>();

                    // if (Classrom.Name != existingClassrom.Name)
                    //     updateDefinitionList.Add(Builders<Classrom>.Update.Set(p => p.Name, Classrom.Name));

                    // if (Classrom.Description != existingClassrom.Description)
                    //     updateDefinitionList.Add(Builders<Classrom>.Update.Set(p => p.Description, Classrom.Description));

                    var update = Builders<Classrom>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Classrom>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _ClassromsCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.ClassromNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Classrom GetById(string id)
        {
            return _ClassromsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Classrom> GetAll(ClassromFilter Filter)
        {
            return _ClassromsCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _ClassromsCollection.DeleteOne(p => p.Id.ToString() == id);

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
