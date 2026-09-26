using MongoDB.Driver;
using TheBabyCowboy.HomeApi.Configuration;
using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Data;

public class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(MongoDbSettings settings)
    {
        var mongoSettings =
            MongoClientSettings.FromConnectionString(
                settings.ConnectionString
            );

        var client = new MongoClient(mongoSettings);

        Database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Thought> Thoughts =>
        Database.GetCollection<Thought>("thoughts");

    public IMongoCollection<Reflection> Reflections =>
        Database.GetCollection<Reflection>("reflections");
}