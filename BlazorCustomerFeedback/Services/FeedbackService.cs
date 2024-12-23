using BlazorCustomerFeedback.Models;

namespace BlazorCustomerFeedback.Services;

public class FeedbackService : IFeedbackService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FeedbackService> _logger;

    public FeedbackService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<FeedbackService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/feedback", feedback);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Feedback>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating feedback");
            throw;
        }
    }

    public async Task DeleteFeedbackAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/feedback/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting feedback with ID {id}");
            throw;
        }
    }

    public Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Feedback> GetFeedbackByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Feedback>($"api/feedback/{id}");
            if (response == null)
            {
                throw new InvalidOperationException($"Feedback with ID {id} not found.");
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching feedback with ID {id}");
            throw;
        }
    }

    public async Task<IEnumerable<Feedback>> GetFeedbackByStatusAsync(FeedbackStatus status)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Feedback>>($"api/feedback/status/{status}");
            return response ?? Enumerable.Empty<Feedback>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching feedback with status {status}");
            throw;
        }
    }

    public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/feedback/{feedback.Id}", feedback);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Feedback>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating feedback with ID {feedback.Id}");
            throw;
        }
    }

}