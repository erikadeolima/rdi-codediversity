using Npgsql;
using AulaTeste20.Models;

namespace AulaTeste20.Data
{
  public sealed class ContactRepository
  {
    private readonly string _connectionString;

    public ContactRepository(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("Default")
          ?? throw new InvalidOperationException("Connection string 'Default' not found.");
    }

    public async Task InitializeAsync()
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            CREATE TABLE IF NOT EXISTS Contacts (
                Id SERIAL PRIMARY KEY,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE,
                CreatedAt TIMESTAMP NOT NULL DEFAULT NOW()
            );
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> CreateAsync(string name, string email)
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            INSERT INTO Contacts (Name, Email, CreatedAt)
            VALUES($1, $2, NOW())
            RETURNING Id;
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      cmd.Parameters.AddWithValue(name);
      cmd.Parameters.AddWithValue(email);

      var result = await cmd.ExecuteScalarAsync();
      return Convert.ToInt32(result);
    }

    public async Task<List<Contact>> GetAllAsync()
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            SELECT Id, Name, Email, CreatedAt
            FROM Contacts
            ORDER BY Id DESC;
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      await using var reader = await cmd.ExecuteReaderAsync();

      var list = new List<Contact>();
      while (await reader.ReadAsync())
      {
        list.Add(new Contact
        {
          Id = reader.GetInt32(0),
          Name = reader.GetString(1),
          Email = reader.GetString(2),
          CreatedAt = reader.GetDateTime(3)
        });
      }

      return list;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            SELECT Id, Name, Email, CreatedAt
            FROM Contacts
            WHERE Id = $1;
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      cmd.Parameters.AddWithValue(id);

      await using var reader = await cmd.ExecuteReaderAsync();
      if (!await reader.ReadAsync())
        return null;

      return new Contact
      {
        Id = reader.GetInt32(0),
        Name = reader.GetString(1),
        Email = reader.GetString(2),
        CreatedAt = reader.GetDateTime(3)
      };
    }

    public async Task<bool> UpdateAsync(int id, string name, string email)
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            UPDATE Contacts
            SET Name = $2,
                Email = $3
            WHERE Id = $1;
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      cmd.Parameters.AddWithValue(id);
      cmd.Parameters.AddWithValue(name);
      cmd.Parameters.AddWithValue(email);

      var affected = await cmd.ExecuteNonQueryAsync();
      return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      await using var connection = new NpgsqlConnection(_connectionString);
      await connection.OpenAsync();

      var sql = """
            DELETE FROM Contacts
            WHERE Id = $1;
            """;

      await using var cmd = new NpgsqlCommand(sql, connection);
      cmd.Parameters.AddWithValue(id);

      var affected = await cmd.ExecuteNonQueryAsync();
      return affected > 0;
    }
  }
}

