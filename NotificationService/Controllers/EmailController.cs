using ILogger = Serilog.ILogger;

namespace NotificationService.Controllers;

[Route("email")]
public class EmailController : BaseController
{
    private readonly IProcessEmailService _processEmail;
    public EmailController(ILogger logger, IProcessEmailService processEmail) : base(logger)
    {
        _processEmail = processEmail;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmailAsync([FromBody] [Bind] EmailMessage request)
    {
        return HandleResponse(await _processEmail.ProcessSendEmailAsync(request));
    }
}