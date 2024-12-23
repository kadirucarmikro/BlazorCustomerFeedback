using BlazorCustomerFeedback.Models;

namespace BlazorCustomerFeedback.Services;

public class CachedFeedbackService : IFeedbackService
{
    private readonly IFeedbackService _feedbackService;
    private readonly ICacheService _cacheService;
    private const string ALL_FEEDBACK_KEY = "all_feedback";
    private const string FEEDBACK_BY_ID_KEY = "feedback_{0}";
    private static readonly TimeSpan _defaultExpiry = TimeSpan.FromMinutes(10);

    public CachedFeedbackService(
        IFeedbackService feedbackService,
        ICacheService cacheService)
    {
        _feedbackService = feedbackService;
        _cacheService = cacheService;
    }
    public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
    {
        return await _cacheService.GetOrSetAsync(
            ALL_FEEDBACK_KEY,
            () => _feedbackService.GetAllFeedbackAsync(),
            _defaultExpiry);
    }

    public async Task<Feedback> GetFeedbackByIdAsync(int id)
    {
        var key = string.Format(FEEDBACK_BY_ID_KEY, id);
        return await _cacheService.GetOrSetAsync(
            key,
            () => _feedbackService.GetFeedbackByIdAsync(id),
            _defaultExpiry);
    }

    public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
    {
        var result = await _feedbackService.CreateFeedbackAsync(feedback);
        await _cacheService.RemoveAsync(ALL_FEEDBACK_KEY);
        return result;
    }

    public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback)
    {
        var result = await _feedbackService.UpdateFeedbackAsync(feedback);
        var key = string.Format(FEEDBACK_BY_ID_KEY, feedback.Id);
        await Task.WhenAll(
            _cacheService.RemoveAsync(ALL_FEEDBACK_KEY),
            _cacheService.RemoveAsync(key));
        return result;
    }

    public async Task DeleteFeedbackAsync(int id)
    {
        await _feedbackService.DeleteFeedbackAsync(id);
        var key = string.Format(FEEDBACK_BY_ID_KEY, id);
        await Task.WhenAll(
            _cacheService.RemoveAsync(ALL_FEEDBACK_KEY),
            _cacheService.RemoveAsync(key));
    }

    public async Task<IEnumerable<Feedback>> GetFeedbackByStatusAsync(FeedbackStatus status)
    {
        var key = $"feedback_status_{status}";
        return await _cacheService.GetOrSetAsync(
            key,
            () => _feedbackService.GetFeedbackByStatusAsync(status),
            _defaultExpiry);
    }
}