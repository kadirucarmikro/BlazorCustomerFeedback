using BlazorCustomerFeedback.Models;
using BlazorCustomerFeedback.Repositories;
using System.Diagnostics;

namespace BlazorCustomerFeedback.Services;

public class PerformanceTestService
{
    private readonly DapperFeedbackRepository _dapperRepo;
    private readonly EFCoreFeedbackRepository _efCoreRepo;
    private readonly ILogger<PerformanceTestService> _logger;

    public PerformanceTestService(
        DapperFeedbackRepository dapperRepo,
        EFCoreFeedbackRepository efCoreRepo,
        ILogger<PerformanceTestService> logger)
    {
        _dapperRepo = dapperRepo;
        _efCoreRepo = efCoreRepo;
        _logger = logger;
    }

    public async Task<PerformanceResult> RunComparisonTest()
    {
        var result = new PerformanceResult();
        var stopwatch = new Stopwatch();

        // Test Dapper Performance
        stopwatch.Start();
        await _dapperRepo.GetAllAsync();
        stopwatch.Stop();
        result.DapperReadTime = stopwatch.ElapsedMilliseconds;

        // Test EF Core Performance
        stopwatch.Restart();
        await _efCoreRepo.GetAllAsync();
        stopwatch.Stop();
        result.EFCoreReadTime = stopwatch.ElapsedMilliseconds;

        // Log results
        _logger.LogInformation(
            "Performance Test Results - Dapper: {DapperTime}ms, EF Core: {EFCoreTime}ms",
            result.DapperReadTime,
            result.EFCoreReadTime);

        return result;
    }
}