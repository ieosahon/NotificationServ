namespace NotificationService.Application.Contract;

public interface IMongoDbLogRepository
{
    Task CreateLog(NotificationActivity request);
}