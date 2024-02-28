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
    public class MatterService : IMatterService
    {
        private readonly IMongoCollection<Matter> _MattersCollection;

        public MatterService(IMongoCollection<Matter> MattersCollection)
        {
            _MattersCollection = MattersCollection;
        }

        public string Register(Matter Matter)
        {
            try
            {
                Matter = Util.FillEntity(Matter);
                _MattersCollection.InsertOne(Matter);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Matter Matter)
        {
            try
            {
                var existingMatter = _MattersCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingMatter != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Matter>>();

                    if (Matter.Name != existingMatter.Name)
                        updateDefinitionList.Add(Builders<Matter>.Update.Set(p => p.Name, Matter.Name));

                    if (Matter.Description != existingMatter.Description)
                        updateDefinitionList.Add(Builders<Matter>.Update.Set(p => p.Description, Matter.Description));

                    var update = Builders<Matter>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Matter>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _MattersCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.MatterNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Matter GetById(string id)
        {
            return _MattersCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Matter> GetAll(MatterFilter Filter)
        {
            return _MattersCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _MattersCollection.DeleteOne(p => p.Id.ToString() == id);

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
