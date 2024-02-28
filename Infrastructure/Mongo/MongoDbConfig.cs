using Domain.Entities;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Mongo
{
    public static class MongoDbConfig
    {
        // Configures MongoDB settings for the application
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration Configuration)
        {
            // Fetch MongoDB connection string and database name from configuration
            string connectionString = Configuration.GetSection("MongoDB:ConectionUrl").Value;
            string databaseName = Configuration.GetSection("MongoDB:DataBaseName").Value;
            
            // Set up MongoDB client and connect to the specified database
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);

            // Register MongoDB collections for Profile and Administrator entities with scoped lifetimes
            services.AddScoped<IMongoCollection<Profile>>(provider => database.GetCollection<Profile>("Profile"));
            services.AddScoped<IMongoCollection<Administrator>>(provider => database.GetCollection<Administrator>("Administrator"));
            services.AddScoped<IMongoCollection<Category>>(provider => database.GetCollection<Category>("Category"));
            services.AddScoped<IMongoCollection<Chat>>(provider => database.GetCollection<Chat>("Chat"));
            services.AddScoped<IMongoCollection<Classrom>>(provider => database.GetCollection<Classrom>("Classrom"));
            services.AddScoped<IMongoCollection<Company>>(provider => database.GetCollection<Company>("Company"));
            services.AddScoped<IMongoCollection<Matter>>(provider => database.GetCollection<Matter>("Matter"));
            services.AddScoped<IMongoCollection<ListActivity>>(provider => database.GetCollection<ListActivity>("ListActivity"));
            services.AddScoped<IMongoCollection<FinancialHistory>>(provider => database.GetCollection<FinancialHistory>("FinancialHistory"));
            services.AddScoped<IMongoCollection<Product>>(provider => database.GetCollection<Product>("Product"));
            services.AddScoped<IMongoCollection<Seller>>(provider => database.GetCollection<Seller>("Seller"));
            services.AddScoped<IMongoCollection<Support>>(provider => database.GetCollection<Support>("Support"));
        
            // Configure Hangfire with MongoDB storage using provided options
            services.AddHangfire(configuration =>
            {
                var options = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new DropMongoMigrationStrategy(),
                        BackupStrategy = new NoneMongoBackupStrategy()
                    },
                    CheckConnection = false
                };
                configuration.UseMongoStorage($"{connectionString}/{databaseName}", options);
            });
        }
    }
}
