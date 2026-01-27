using System;
using System.Linq;

namespace Classes
{
  public class MenuSystem(UserRepository repository)
  {
    public readonly UserRepository _repository = repository;

    public void Execute()
    {
      bool shouldContinue = true;
      while (shouldContinue)
      {
        Console.Clear();
        ShowMenu();

        string option = Console.ReadLine()?.Trim();

        shouldContinue = ProcessOption(option);

        if (shouldContinue)
          WaitKeyBoard();
      }
    }

    private static void ShowMenu()
    {
      Console.WriteLine("=== SISTEMA DE USUÁRIOS ===\n");
      Console.WriteLine("1. Cadastrar usuário");
      Console.WriteLine("2. Listar usuários");
      Console.WriteLine("3. Buscar usuário");
      Console.WriteLine("4. Sair");
      Console.Write("\nEscolha (1-4): ");
    }

    private bool ProcessOption(string option)
    {
      return option switch
      {
        "1" => Registry(),
        "2" => List(),
        "3" => Search(),
        "4" => false,
        _ => ShowInvalidOption()
      };
    }
    private bool ShowInvalidOption()
    {
      Console.WriteLine("❌ Opção inválida!");
      return true;
    }

    private bool Registry()
    {
      Console.Clear();
      Console.WriteLine("=== Cadastrar ===\n");
      string name = ReadString("Nome");
      int age = LerInteiro("Age");

      if (age <= 0)
        return true;

      _repository.Registry(new User(name, age));

      Console.WriteLine($"\n '{name}' cadastrado!");
      return true;
    }
    private bool List()
    {
      Console.Clear();
      Console.WriteLine("=== LISTA ===\n");

      var users = _repository.ListAll();
      if (!users.Any())
      {
        Console.WriteLine("Nenhum usuário cadastrado.");
        return true;
      }

      foreach (var u in users)
        Console.WriteLine($"{u.name} - {u.age} anos");

      return true;
    }

    private bool Search()
    {
      Console.Clear();
      Console.WriteLine("=== BUSCAR ===\n");

      string name = ReadString("Nome para buscar");
      var users = _repository.SearchForName(name);

      if (!users.Any())
      {
        Console.WriteLine($"Nenhum usuário encontrado.");
        return true;
      }

      Console.WriteLine("\nEncontrados:");
      foreach (var u in users)
        Console.WriteLine($"{u.name} - {u.age} anos");

      return true;
    }

    private static string ReadString(string field)
    {
      Console.Write($"{field}: ");
      return Console.ReadLine()?.Trim() ?? "";
    }

    private static int LerInteiro(string field)
    {
      Console.Write($"{field}: ");
      return int.TryParse(Console.ReadLine(), out int value) ? value : 0;
    }

    private static void WaitKeyBoard()
    {
      Console.WriteLine("\nPressione qualquer tecla...");
      Console.ReadKey();
    }
  }
}