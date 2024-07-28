namespace NotificationService.Application.Contract;

public interface IMongoDbConfig
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}