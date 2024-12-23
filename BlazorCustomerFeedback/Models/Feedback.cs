namespace BlazorCustomerFeedback.Models;

public class Feedback
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; }
    public FeedbackStatus Status { get; set; }
}

public enum FeedbackStatus
{
    New,
    InProgress,
    Resolved,
    Closed
}