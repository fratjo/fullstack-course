using System.Data.SqlClient;

namespace Devoir.Repositories.SqlConnectionFactory;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public async Task<SqlConnection> GetOpenAsyncSqlConnection()
    {
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }
}