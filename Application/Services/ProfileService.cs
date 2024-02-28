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
    public class ProfileService : IProfileService
    {
        private readonly IMongoCollection<Profile> _profilesCollection;

        public ProfileService(IMongoCollection<Profile> profilesCollection)
        {
            _profilesCollection = profilesCollection;
        }

        public string Register(Profile profile)
        {
            try
            {
                profile = Util.FillEntity(profile);
                profile.Password = Util.GerarHashMd5(profile.Password);
                _profilesCollection.InsertOne(profile);

                return Messages.SuccessfulDataRegistration;
            }
            catch (Exception)
            {
                //return $"Erro ao inserir no banco de dados: {ex.Message}";
                return Messages.DatabaseError;
            }
        }
        public string Update(string id, Profile profile)
        {
            try
            {
                var existingProfile = _profilesCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();

                if (existingProfile != null)
                {
                    var updateDefinitionList = new List<UpdateDefinition<Profile>>();

                    if (profile.FullName != existingProfile.FullName)
                        updateDefinitionList.Add(Builders<Profile>.Update.Set(p => p.FullName, profile.FullName));

                    if (profile.Email != existingProfile.Email)
                        updateDefinitionList.Add(Builders<Profile>.Update.Set(p => p.Email, profile.Email));

                    if (profile.Password != existingProfile.Password)
                        updateDefinitionList.Add(Builders<Profile>.Update.Set(p => p.Password, Util.GerarHashMd5(profile.Password)));

                    if (profile.DateBirth != existingProfile.DateBirth)
                        updateDefinitionList.Add(Builders<Profile>.Update.Set(p => p.DateBirth, profile.DateBirth));

                    if (profile.TypeProfile != existingProfile.TypeProfile)
                        updateDefinitionList.Add(Builders<Profile>.Update.Set(p => p.TypeProfile, profile.TypeProfile));

                    var update = Builders<Profile>.Update.Combine(updateDefinitionList);

                    var filter = Builders<Profile>.Filter.Eq(p => p.Id.ToString(), id);

                    var result = _profilesCollection.UpdateOne(filter, update);

                    return Messages.SuccessfulDataUpdate;
                }
                else
                {
                    return Messages.ProfileNotfull;
                }
            }
            catch (Exception)
            {
                return Messages.DatabaseError;
            }
        }


        public Profile GetById(string id)
        {
            return _profilesCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Profile> GetAll(ProfileFilter Filter)
        {
            return _profilesCollection.Find(_ => true).ToList();
        }

        public string Delete(string id)
        {
            try
            {
                _profilesCollection.DeleteOne(p => p.Id.ToString() == id);

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
