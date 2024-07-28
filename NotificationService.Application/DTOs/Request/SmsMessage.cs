namespace NotificationService.Application.DTOs.Request;

public class SmsMessage
{
    public string To { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
}