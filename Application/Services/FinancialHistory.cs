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
    public class FinancialHistoryService : IFinancialHistoryService
    {
        private readonly IMongoCollection<FinancialHistory> _FinancialHistorysCollection;

        public FinancialHistoryService(IMongoCollection<FinancialHistory> FinancialHistorysCollection)
        {
            _FinancialHistorysCollection = FinancialHistorysCollection;
        }

        public string Register(FinancialHistory FinancialHistory)
        {
            try
            {
                FinancialHistory = Util.FillEntity(FinancialHistory);
                _FinancialHistorysCollection.InsertOne(FinancialHistory);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, FinancialHistory FinancialHistory)
        {
            try
            {
                var existingFinancialHistory = _FinancialHistorysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingFinancialHistory != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<FinancialHistory>>();

                    // if (FinancialHistory.Name != existingFinancialHistory.Name)
                    //     updateDefinitionList.Add(Builders<FinancialHistory>.Update.Set(p => p.Name, FinancialHistory.Name));

                    // if (FinancialHistory.Description != existingFinancialHistory.Description)
                    //     updateDefinitionList.Add(Builders<FinancialHistory>.Update.Set(p => p.Description, FinancialHistory.Description));

                    var update = Builders<FinancialHistory>.Update.Combine(updateDefinitionList);

                    var filter = Builders<FinancialHistory>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _FinancialHistorysCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.FinancialHistoryNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public FinancialHistory GetById(string id)
        {
            return _FinancialHistorysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<FinancialHistory> GetAll(FinancialHistoryFilter Filter)
        {
            return _FinancialHistorysCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _FinancialHistorysCollection.DeleteOne(p => p.Id.ToString() == id);

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
