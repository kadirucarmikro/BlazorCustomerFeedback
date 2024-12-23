using BlazorCustomerFeedback.Models;

namespace BlazorCustomerFeedback.Services;

public interface IFeedbackService
{
    Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
    Task<Feedback> GetFeedbackByIdAsync(int id);
    Task<Feedback> CreateFeedbackAsync(Feedback feedback);
    Task<Feedback> UpdateFeedbackAsync(Feedback feedback);
    Task DeleteFeedbackAsync(int id);
    Task<IEnumerable<Feedback>> GetFeedbackByStatusAsync(FeedbackStatus status);
}