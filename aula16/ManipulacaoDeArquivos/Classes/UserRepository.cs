using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Classes
{
  public class UserRepository
  {
    private readonly string _pathOfArchive;

    public UserRepository()
    {
      _pathOfArchive = Path.Combine("dados", "users.csv");
      Directory.CreateDirectory(Path.GetDirectoryName(_pathOfArchive)!);
    }

    public void Registry(User user)
    {
      File.AppendAllText(_pathOfArchive, $"{user.name}, {user.age}\n");
    }

    public List<User> ListAll()
    {
      if (!File.Exists(_pathOfArchive))
        return new List<User>();

      return File.ReadAllLines(_pathOfArchive)
                .Where(ValidLine)
                .Select(ParseLine)
                .ToList();
    }

    public List<User> SearchForName(string name)
    {
      return ListAll()
        .Where(u => u.name.Contains(name, StringComparison.OrdinalIgnoreCase))
        .ToList();
    }

    private static bool ValidLine(string line) =>
        !string.IsNullOrWhiteSpace(line) && line.Contains(',');

    private static User ParseLine(string line)
    {
      var parts = line.Split(',');
      return new User(parts[0].Trim(), int.Parse(parts[1].Trim()));
    }
  }
}