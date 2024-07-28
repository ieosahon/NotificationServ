namespace NotificationService.Application.Contract;

public interface IProcessEmailService
{
    Task<Result<string>> ProcessSendEmailAsync(EmailMessage request);
}