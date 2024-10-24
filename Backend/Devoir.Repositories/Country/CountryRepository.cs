using System.Data.SqlClient;
using Devoir.CustomException;
using Devoir.Repositories.SqlConnectionFactory;
using Devoir.Repositories.Errors;

namespace Devoir.Repositories.Country;

public class CountryRepository(ISqlConnectionFactory sqlConnectionFactory) : ICountryRepository
{
    #region Gets 
    public async Task<IEnumerable<Devoir.Models.Country>> GetAllAsync()
    {
        var countries = new List<Devoir.Models.Country>();
        
        try
        {
            await using var connection = await sqlConnectionFactory.GetOpenAsyncSqlConnection();
            await using var command = new SqlCommand("SELECT * FROM Countries; ", connection);
            await using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var country = new Devoir.Models.Country
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Short = reader.GetString(2)
                };
                countries.Add(country);
            }
        }
        catch (Exception e)
        {
            throw e.MapToApplicationException();            
        }
        
        return countries;
    }

    public async Task<Devoir.Models.Country?> GetByIdAsync(int id)
    {
        Devoir.Models.Country? country = null;
        
        try
        {
            await using var connection = await sqlConnectionFactory.GetOpenAsyncSqlConnection();
            await using var command = new SqlCommand("SELECT * FROM Countries WHERE country_id = @Id; ", connection);
            
            command.Parameters.AddWithValue("@Id", id);
            
            await using var reader = await command.ExecuteReaderAsync(); // reader after parameters
            
            
            if (await reader.ReadAsync())
            {
                country = new Devoir.Models.Country()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Short = reader.GetString(2)
                };
            }
        }
        catch (Exception e)
        {
            throw e.MapToApplicationException();
        }

        return country;
    }

    #endregion
    
    #region Posts
    public async Task<int> AddAsync(Devoir.Models.CountryInsertRequest entity)
    {
        object? id;
        
        try
        {
            await using var connection = await sqlConnectionFactory.GetOpenAsyncSqlConnection();
            await using var command = new SqlCommand(
                """
                    INSERT INTO Countries (country_name, country_name_short) VALUES (@Name, @Short);
                    SELECT SCOPE_IDENTITY();
                """, connection);
            
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Short", entity.Short);
            
            id = await command.ExecuteScalarAsync();
        }
        catch (Exception e)
        {
            throw e.MapToApplicationException();          
        }
        
        return id is not null ? Convert.ToInt32(id) : -1;
    }

    #endregion
    
    #region Puts
    public async Task<bool> UpdateAsync(int id, Devoir.Models.Country entity)
    {
        int rows;
        
        try
        {
            await using var connection = await sqlConnectionFactory.GetOpenAsyncSqlConnection();
            await using var command = new SqlCommand(
                "UPDATE Countries SET country_name = @Name, country_name_short = @Short WHERE country_id = @Id;",
                connection);
            
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Short", entity.Short);
            
            rows = await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            throw e.MapToApplicationException();           
        }
        
        return rows > 0;
    }

    #endregion
    
    #region Deletes
    public async Task<bool> DeleteAsync(int id)
    {
        int rows;
        
        try
        {
            await using var connection = await sqlConnectionFactory.GetOpenAsyncSqlConnection();
            await using var command = new SqlCommand("DELETE FROM Countries WHERE country_id = @Id; ", connection);
            
            command.Parameters.AddWithValue("@Id", id);
            
            rows = await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            throw e.MapToApplicationException();             
        }
        
        return rows > 0;
    }
    
    #endregion
}
