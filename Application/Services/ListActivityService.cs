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
    public class ListActivityService : IListActivityService
    {
        private readonly IMongoCollection<ListActivity> _ListActivitysCollection;

        public ListActivityService(IMongoCollection<ListActivity> ListActivitysCollection)
        {
            _ListActivitysCollection = ListActivitysCollection;
        }

        public string Register(ListActivity ListActivity)
        {
            try
            {
                ListActivity = Util.FillEntity(ListActivity);
                _ListActivitysCollection.InsertOne(ListActivity);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, ListActivity ListActivity)
        {
            try
            {
                var existingListActivity = _ListActivitysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingListActivity != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<ListActivity>>();

                    // if (ListActivity.Name != existingListActivity.Name)
                    //     updateDefinitionList.Add(Builders<ListActivity>.Update.Set(p => p.Name, ListActivity.Name));

                    // if (ListActivity.Description != existingListActivity.Description)
                    //     updateDefinitionList.Add(Builders<ListActivity>.Update.Set(p => p.Description, ListActivity.Description));

                    var update = Builders<ListActivity>.Update.Combine(updateDefinitionList);

                    var filter = Builders<ListActivity>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _ListActivitysCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.ListActivityNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public ListActivity GetById(string id)
        {
            return _ListActivitysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<ListActivity> GetAll(ListActivityFilter Filter)
        {
            return _ListActivitysCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _ListActivitysCollection.DeleteOne(p => p.Id.ToString() == id);

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
