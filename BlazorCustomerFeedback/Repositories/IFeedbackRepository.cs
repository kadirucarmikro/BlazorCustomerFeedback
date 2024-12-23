using BlazorCustomerFeedback.Models;

namespace BlazorCustomerFeedback.Repositories;

public interface IFeedbackRepository
{
    Task<IEnumerable<Feedback>> GetAllAsync();
    Task<Feedback> GetByIdAsync(int id);
    Task<int> CreateAsync(Feedback feedback);
    Task<bool> UpdateAsync(Feedback feedback);
    Task<bool> DeleteAsync(int id);
}