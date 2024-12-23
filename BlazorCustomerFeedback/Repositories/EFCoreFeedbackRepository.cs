using BlazorCustomerFeedback.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCustomerFeedback.Repositories;


public class EFCoreFeedbackRepository : IFeedbackRepository
{
    private readonly FeedbackDbContext _context;

    public EFCoreFeedbackRepository(FeedbackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        return await _context.Feedback.ToListAsync();
    }

    public async Task<Feedback> GetByIdAsync(int id)
    {
        return await _context.Feedback.FindAsync(id);
    }

    public async Task<int> CreateAsync(Feedback feedback)
    {
        _context.Feedback.Add(feedback);
        await _context.SaveChangesAsync();
        return feedback.Id;
    }

    public async Task<bool> UpdateAsync(Feedback feedback)
    {
        _context.Entry(feedback).State = EntityState.Modified;
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var feedback = await _context.Feedback.FindAsync(id);
        if (feedback == null) return false;

        _context.Feedback.Remove(feedback);
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }
}