namespace BlazorCustomerFeedback.Models;

public class PerformanceResult
{
    public long DapperReadTime { get; set; }
    public long EFCoreReadTime { get; set; }
    public string Summary => $"Dapper: {DapperReadTime}ms | EF Core: {EFCoreReadTime}ms";
}