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
    public class CompanyService : ICompanyService
    {
        private readonly IMongoCollection<Company> _CompanysCollection;

        public CompanyService(IMongoCollection<Company> CompanysCollection)
        {
            _CompanysCollection = CompanysCollection;
        }

        public string Register(Company Company)
        {
            try
            {
                Company = Util.FillEntity(Company);
                _CompanysCollection.InsertOne(Company);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Company Company)
        {
            try
            {
                var existingCompany = _CompanysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingCompany != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Company>>();

                    // if (Company.Name != existingCompany.Name)
                    //     updateDefinitionList.Add(Builders<Company>.Update.Set(p => p.Name, Company.Name));

                    // if (Company.Description != existingCompany.Description)
                    //     updateDefinitionList.Add(Builders<Company>.Update.Set(p => p.Description, Company.Description));

                    var update = Builders<Company>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Company>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _CompanysCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.CompanyNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Company GetById(string id)
        {
            return _CompanysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Company> GetAll(CompanyFilter Filter)
        {
            return _CompanysCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _CompanysCollection.DeleteOne(p => p.Id.ToString() == id);

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
