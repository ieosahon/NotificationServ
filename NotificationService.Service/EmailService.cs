global using MailKit.Security;
global using Microsoft.Extensions.Configuration;
global using MimeKit;
global using NotificationService.Application.DTOs.Request;
global using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NotificationService.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<int> SendEmailAsync(EmailMessage message)
    {
        var res = 0;
        var emailMessage = new MimeMessage();
        try
        {
            emailMessage.From.Add(new MailboxAddress(_config["MailConfig:Name"], _config["MailConfig:Sender"]));

            foreach (var recipient in message.To)
            {
                emailMessage.To.Add(new MailboxAddress("", recipient));
            }

            if (message.Cc != null)
            {
                foreach (var cc in message.Cc.Where(cc => !string.IsNullOrEmpty(cc)))
                {
                    emailMessage.Cc.Add(new MailboxAddress("", cc));
                }
            }

            if (message.Bcc != null)
            {
                foreach (var bcc in message.Bcc.Where(bcc => !string.IsNullOrEmpty(bcc)))
                {
                    emailMessage.Bcc.Add(new MailboxAddress("", bcc));
                }
            }

            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.Body
            };

            if (message.Attachments != null)
                foreach (var attachment in message.Attachments)
                {
                    if (string.IsNullOrEmpty(attachment) || !File.Exists(attachment))
                        continue;
                    var fileName = Path.GetFileName(attachment);
                    //bodyBuilder.Attachments.Add(attachment, fileName);
                    await using var stream = File.OpenRead(attachment);
                    await bodyBuilder.Attachments.AddAsync(fileName, stream);
                }


            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["MailConfig:SmtpServer"], Convert.ToInt32(_config["MailConfig:Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config["MailConfig:Sender"], _config["MailConfig:Password"]);
            var send = await client.SendAsync(emailMessage);
            res = send.Contains("OK") ? 1 : 0;
            await client.DisconnectAsync(true);
            return res;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return res;
        }
    }
}