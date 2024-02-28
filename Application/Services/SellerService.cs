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
    public class SellerService : ISellerService
    {
        private readonly IMongoCollection<Seller> _SellersCollection;

        public SellerService(IMongoCollection<Seller> SellersCollection)
        {
            _SellersCollection = SellersCollection;
        }

        public string Register(Seller Seller)
        {
            try
            {
                Seller = Util.FillEntity(Seller);
                _SellersCollection.InsertOne(Seller);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Seller Seller)
        {
            try
            {
                var existingSeller = _SellersCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingSeller != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Seller>>();

                    // if (Seller.Name != existingSeller.Name)
                    //     updateDefinitionList.Add(Builders<Seller>.Update.Set(p => p.Name, Seller.Name));

                    // if (Seller.Description != existingSeller.Description)
                    //     updateDefinitionList.Add(Builders<Seller>.Update.Set(p => p.Description, Seller.Description));

                    var update = Builders<Seller>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Seller>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _SellersCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.SellerNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Seller GetById(string id)
        {
            return _SellersCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Seller> GetAll(SellerFilter Filter)
        {
            return _SellersCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _SellersCollection.DeleteOne(p => p.Id.ToString() == id);

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
