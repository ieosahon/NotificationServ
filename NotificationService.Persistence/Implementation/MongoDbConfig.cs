namespace NotificationService.Persistence.Implementation;

public class MongoDbConfig : IMongoDbConfig
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }

    public MongoDbConfig(string ConnectionString, string DatabaseName)
    {
        this.DatabaseName = DatabaseName;
        this.ConnectionString = ConnectionString;
    }
}