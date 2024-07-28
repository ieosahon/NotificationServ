using NotificationService.Domain.Entity;
using NotificationService.Domain.Enums;
using Serilog;

namespace NotificationService.Service;

public class ProcessEmailService : IProcessEmailService
{
    private readonly IEmailService _emailService;
    private readonly IMongoDbLogRepository _dbLogRepository;
    private readonly ILogger _logger;

    public ProcessEmailService(IEmailService emailService, IMongoDbLogRepository dbLogRepository, ILogger logger)
    {
        _emailService = emailService;
        _dbLogRepository = dbLogRepository;
        _logger = logger;
    }

    public async Task<Result<string>> ProcessSendEmailAsync(EmailMessage request)
    {
        var result = new Result<string>();
        
        try
        {
            var response = await _emailService.SendEmailAsync(request);
            
            var dbObject = new NotificationActivity
            {
                To = request.To,
                HasAttachment = true,
                NotificationType = NotificationType.Email.ToString(),
                Purpose = request.Purpose,
                IsNotificationSent = response,
                RequestedBy = "Tester",
                Bcc = request.Bcc,
                Cc = request.Cc,
                Subject = request.Subject,
            };
            if (request.Attachments is null)
            {
                dbObject.HasAttachment = false;
            }

            await _dbLogRepository.CreateLog(dbObject);
            if (response == 1)
                return new Result<string>
                {
                    ResponseCode = "200",
                    ResponseMsg = "Email sent successfully."
                };
            _logger.Information("Unable to send email");
            return new Result<string>
            {
                ResponseCode = "400",
                ResponseMsg = "Unable to send email"
            };

        }
        catch (Exception ex)
        {
            _logger.Error($"Err from {nameof(ProcessSendEmailAsync)} ==> {ex.Message}; st ==> {ex.StackTrace}");
            return new Result<string>
            {
                ResponseCode = "500",
                ResponseMsg = "An error occurred, please check your internet and try again."
            };
        }
    }
}