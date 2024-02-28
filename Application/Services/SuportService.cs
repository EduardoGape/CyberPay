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
    public class SupportService : ISupportService
    {
        private readonly IMongoCollection<Support> _SupportsCollection;

        public SupportService(IMongoCollection<Support> SupportsCollection)
        {
            _SupportsCollection = SupportsCollection;
        }

        public string Register(Support Support)
        {
            try
            {
                Support = Util.FillEntity(Support);
                _SupportsCollection.InsertOne(Support);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Support Support)
        {
            try
            {
                var existingSupport = _SupportsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingSupport != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Support>>();

                    // if (Support.Name != existingSupport.Name)
                    //     updateDefinitionList.Add(Builders<Support>.Update.Set(p => p.Name, Support.Name));

                    // if (Support.Description != existingSupport.Description)
                    //     updateDefinitionList.Add(Builders<Support>.Update.Set(p => p.Description, Support.Description));

                    var update = Builders<Support>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Support>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _SupportsCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.SupportNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Support GetById(string id)
        {
            return _SupportsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Support> GetAll(SupportFilter Filter)
        {
            return _SupportsCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _SupportsCollection.DeleteOne(p => p.Id.ToString() == id);

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
