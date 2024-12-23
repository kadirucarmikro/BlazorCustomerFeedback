using BlazorCustomerFeedback.Data;
using BlazorCustomerFeedback.Models;
using Dapper;

namespace BlazorCustomerFeedback.Repositories;


public class DapperFeedbackRepository : IFeedbackRepository
{
    private readonly IDapperContext _context;

    public DapperFeedbackRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Feedback>("SELECT * FROM Feedback");
    }

    public async Task<Feedback> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Feedback>(
            "SELECT * FROM Feedback WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<int> CreateAsync(Feedback feedback)
    {
        var sql = @"INSERT INTO Feedback (Title, Description, Rating, Category, CreatedAt, UserEmail, Status) 
                    VALUES (@Title, @Description, @Rating, @Category, @CreatedAt, @UserEmail, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(sql, feedback);
    }

    public async Task<bool> UpdateAsync(Feedback feedback)
    {
        var sql = @"UPDATE Feedback 
                    SET Title = @Title, 
                        Description = @Description,
                        Rating = @Rating,
                        Category = @Category,
                        Status = @Status
                    WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, feedback);
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Feedback WHERE Id = @Id",
            new { Id = id });
        return affected > 0;
    }
}