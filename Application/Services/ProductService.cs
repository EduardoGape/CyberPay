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
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _ProductsCollection;

        public ProductService(IMongoCollection<Product> ProductsCollection)
        {
            _ProductsCollection = ProductsCollection;
        }

        public string Register(Product Product)
        {
            try
            {
                Product = Util.FillEntity(Product);
                _ProductsCollection.InsertOne(Product);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Product Product)
        {
            try
            {
                var existingProduct = _ProductsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingProduct != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Product>>();

                    // if (Product.Name != existingProduct.Name)
                    //     updateDefinitionList.Add(Builders<Product>.Update.Set(p => p.Name, Product.Name));

                    // if (Product.Description != existingProduct.Description)
                    //     updateDefinitionList.Add(Builders<Product>.Update.Set(p => p.Description, Product.Description));

                    var update = Builders<Product>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Product>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _ProductsCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.ProductNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Product GetById(string id)
        {
            return _ProductsCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll(ProductFilter Filter)
        {
            return _ProductsCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _ProductsCollection.DeleteOne(p => p.Id.ToString() == id);

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
