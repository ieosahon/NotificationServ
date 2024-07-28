global using MongoDB.Driver;
global using NotificationService.Domain.Entity;

namespace NotificationService.Application.Contract;

public interface IMongoDbContext
{
    IMongoCollection<NotificationActivity> MongoLog { get; set; }
}