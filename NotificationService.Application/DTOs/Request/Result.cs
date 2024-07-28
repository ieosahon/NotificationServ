// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace NotificationService.Application.DTOs.Request;

public class Result
{
    public string ResponseCode { get; set; }
    public string ResponseMsg { get; set; }
    public PaginationDetails PaginationDetails { get; set; }
}

public class PaginationDetails
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
}
public class Result<T> : Result
{
    public T ResponseDetails { get; set; }
    
}