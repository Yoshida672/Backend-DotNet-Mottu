using Backend_Dotnet_Mottu.Application.Configs;
using MongoDB.Driver;

namespace Backend_Dotnet_Mottu.Infrastructure;

public class MongoContext
{
    public MongoContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.DatabaseName);    
    }
    
    public IMongoDatabase Database { get; }
}