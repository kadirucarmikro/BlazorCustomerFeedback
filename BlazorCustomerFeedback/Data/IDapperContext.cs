using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorCustomerFeedback.Data;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}

public class DapperContext : IDapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}