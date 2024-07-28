// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace NotificationService.Domain.Entity;

public class NotificationActivity
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public List<string> To { get; set; }
    public string Subject { get; set; }
    public bool HasAttachment { get; set; }
    public string NotificationType { get; set; }
    public List<string> Cc { get; set; }
    public List<string> Bcc { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow.AddHours(1);
    public string RequestedBy { get; set; }
    public string Purpose { get; set; }
    public int IsNotificationSent { get; set; }

}
