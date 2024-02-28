using Application.IServices;
using Domain.Entities;
using Domain.Filters;
using Hangfire;
using Hangfire.Mongo;
using Infrastructure.Messages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IMongoCollection<Administrator> _administratorsCollection;

        public AdministratorService(IMongoCollection<Administrator> administratorsCollection)
        {
            _administratorsCollection = administratorsCollection;
        }

        public string Register(Administrator administrator)
        {
            try
            {
                administrator = Util.FillEntity(administrator);
                administrator.Password = Util.GerarHashMd5(administrator.Password);
                _administratorsCollection.InsertOne(administrator);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Administrator administrator)
        {
            try
            {
                var existingadministrator = _administratorsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingadministrator != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Administrator>>();

                    if (administrator.FullName != existingadministrator.FullName)
                        updateDefinitionList.Add(Builders<Administrator>.Update.Set(p => p.FullName, administrator.FullName));

                    if (administrator.Email != existingadministrator.Email)
                        updateDefinitionList.Add(Builders<Administrator>.Update.Set(p => p.Email, administrator.Email));

                    if (administrator.Password != existingadministrator.Password)
                        updateDefinitionList.Add(Builders<Administrator>.Update.Set(p => p.Password, Util.GerarHashMd5(administrator.Password)));

                    if (administrator.TypeProfile != existingadministrator.TypeProfile)
                        updateDefinitionList.Add(Builders<Administrator>.Update.Set(p => p.TypeProfile, administrator.TypeProfile));

                    var update = Builders<Administrator>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Administrator>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _administratorsCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.AdministratorNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Administrator GetById(string id)
        {
            return _administratorsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Administrator> GetAll(AdministratorFilter Filter)
        {
            return _administratorsCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _administratorsCollection.DeleteOne(p => p.Id.ToString() == id);

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
