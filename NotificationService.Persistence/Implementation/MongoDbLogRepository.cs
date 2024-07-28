global using Serilog;

namespace NotificationService.Persistence.Implementation;

public class MongoDbLogRepository : IMongoDbLogRepository
{
    private readonly IMongoDbContext _context;
    private readonly ILogger _logger;

    public MongoDbLogRepository(IMongoDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task CreateLog(NotificationActivity request)
    {
        try
        {
            await _context.MongoLog.InsertOneAsync(request);
           
        }
        catch (Exception ex)
        {
             _logger.Error($"Error from log repository ==> {ex.Message}; stack trace error == > {ex.StackTrace}");
        }
    }
}