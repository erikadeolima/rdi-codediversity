using AdoNetProjeto.Models;
using Microsoft.Data.Sqlite;

namespace AdoNetProjeto.Data
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
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            CREATE TABLE IF NOT EXISTS Contacts (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );

            CREATE UNIQUE INDEX IF NOT EXISTS IX_Contacts_Email ON Contacts(Email);
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> CreateAsync(string name, string email)
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            INSERT INTO Contacts (Name, Email, CreatedAt)
            VALUES($name, $email, $createdAt);
            SELECT last_insert_rowid();
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$email", email);
            cmd.Parameters.AddWithValue("$createdAt", DateTime.UtcNow.ToString("O"));

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);

        }

        public async Task<List<Contact>> GetAllAsync()
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            SELECT Id, Name, Email, CreatedAt
            FROM Contacts
            ORDER BY Id DESC;
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            await using var reader = await cmd.ExecuteReaderAsync();

            var list = new List<Contact>();
            while (await reader.ReadAsync())
            {
                list.Add(new Contact
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    CreatedAt = DateTime.Parse(reader.GetString(3))
                });
            }

            return list;
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            SELECT Id, Name, Email, CreatedAt
            FROM Contacts
            WHERE Id = $id;
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("$id", id);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return new Contact
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                CreatedAt = DateTime.Parse(reader.GetString(3))
            };
        }

        public async Task<bool> UpdateAsync(int id, string name, string email)
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            UPDATE Contacts
            SET Name = $name,
                Email = $email
            WHERE Id = $id;
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("$id", id);
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$email", email);

            var affected = await cmd.ExecuteNonQueryAsync();
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = """
            DELETE FROM Contacts
            WHERE Id = $id;
            """;

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("$id", id);

            var affected = await cmd.ExecuteNonQueryAsync();
            return affected > 0;
        }
    }
}
