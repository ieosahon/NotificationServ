global using Microsoft.Extensions.DependencyInjection;
global using NotificationService.Application.Contract;

namespace NotificationService.Service;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IProcessEmailService, ProcessEmailService>();
        return services;
    }
}