using ILogger = Serilog.ILogger;

namespace NotificationService.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private readonly ILogger _logger;
    public BaseController(ILogger logger)
    {
        _logger = logger;
    }

    internal IActionResult HandleResponse<T>(Result<T> result)
    {
        return result.ResponseCode switch
        {
            "200" => Ok(result),
            "400" => BadRequest(result),
            "401" => Unauthorized(result),
            "404" => NotFound(result),
            "407" => Conflict(result),
            _ => StatusCode(500, result)
        };
    }
}