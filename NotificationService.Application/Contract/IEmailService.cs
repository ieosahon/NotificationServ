namespace NotificationService.Application.Contract;

public interface IEmailService
{
    Task<int> SendEmailAsync(EmailMessage message);
}