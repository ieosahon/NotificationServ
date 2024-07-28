global using MongoDB.Driver;
global using NotificationService.Application.Contract;
global using NotificationService.Domain.Entity;

namespace NotificationService.Persistence.Implementation;

public class MongoDbContext : IMongoDbContext
{
    public IMongoCollection<NotificationActivity> MongoLog { get; set; }

    public MongoDbContext(IMongoDbConfig config)
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDB"));
        var database = client.GetDatabase(config.DatabaseName);

        MongoLog = database.GetCollection<NotificationActivity>("NotificationLog");
    }
}