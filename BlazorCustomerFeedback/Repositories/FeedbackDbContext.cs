using BlazorCustomerFeedback.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCustomerFeedback.Repositories;

public class FeedbackDbContext : DbContext
{
    // Constructor that accepts database configuration options
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options)
        : base(options)
    {
    }

    // Defines the Feedback table in the database
    public DbSet<Feedback> Feedback { get; set; }

    // Configures special model mappings
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configures the Status enum to be stored as a string in the database
        modelBuilder.Entity<Feedback>()
            .Property(f => f.Status)
            .HasConversion<string>();
    }
}
