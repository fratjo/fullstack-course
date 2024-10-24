using System.Data.SqlClient;

namespace Devoir.Repositories.SqlConnectionFactory;

public interface ISqlConnectionFactory
{
    Task<SqlConnection> GetOpenAsyncSqlConnection();
}