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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categorysCollection;

        public CategoryService(IMongoCollection<Category> CategorysCollection)
        {
            _categorysCollection = CategorysCollection;
        }

        public string Register(Category Category)
        {
            try
            {
                Category = Util.FillEntity(Category);
                _categorysCollection.InsertOne(Category);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Category Category)
        {
            try
            {
                var existingCategory = _categorysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingCategory != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Category>>();

                    if (Category.Name != existingCategory.Name)
                        updateDefinitionList.Add(Builders<Category>.Update.Set(p => p.Name, Category.Name));

                    if (Category.Description != existingCategory.Description)
                        updateDefinitionList.Add(Builders<Category>.Update.Set(p => p.Description, Category.Description));

                    var update = Builders<Category>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Category>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _categorysCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.CategoryNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Category GetById(string id)
        {
            return _categorysCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll(CategoryFilter Filter)
        {
            return _categorysCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _categorysCollection.DeleteOne(p => p.Id.ToString() == id);

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
