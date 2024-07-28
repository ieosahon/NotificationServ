using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Persistence.Implementation;
#pragma warning disable CS8604 // Possible null reference argument.

namespace NotificationService.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services , IConfiguration configuration)
    {
         services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Environment.GetEnvironmentVariable("MongoDB")));
         services.AddSingleton<IMongoDbConfig, MongoDbConfig>(sp
             => new MongoDbConfig(Environment.GetEnvironmentVariable("MongoDB"),
             configuration.GetSection("MongoDbSettings:DatabaseName").Value));
         services.AddScoped<IMongoDbContext, MongoDbContext>();
         services.AddScoped<IMongoDbLogRepository, MongoDbLogRepository>();
         return services;
    }

}