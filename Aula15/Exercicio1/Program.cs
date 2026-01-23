using System;
using Classes;

class Program
{
  public static void Main(string[] args)
  {
    bool shouldContinue = true;
    while (shouldContinue)
    {
      Console.Clear();
      Console.WriteLine("=== TRANSPORT COST CALCULATOR ===\n");
      Console.WriteLine("1. Car");
      Console.WriteLine("2. Bike");
      Console.WriteLine("3. Bus");
      Console.WriteLine("4. Exit");

      Console.WriteLine("\nChoose trnsport (1-4): ");
      string opcao = Console.ReadLine();

      if (opcao == "4")
      {
        shouldContinue = false;
        Console.WriteLine("Bye!");
        break;
      }

      if (opcao != "1" && opcao != "2" && opcao != "3")
      {
        Console.WriteLine("\n❌ Invalid option! Choose 1, 2, 3 or 4.");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
        continue;
      }

      Console.Write("Enter distance (km): ");
      if (!decimal.TryParse(Console.ReadLine(), out decimal distance) || distance <= 0)
      {
        Console.WriteLine("Invalid distance! Press any key...");
        Console.ReadKey();
        continue;
      }

      Transport transport = opcao switch
      {
        "1" => new Car(),
        "2" => new Bike(),
        "3" => new Bus(),
        _ => throw new InvalidOperationException("Invalid option")
      };

      transport.Move();
      decimal cost = transport.CalculateCost(distance);
      Console.WriteLine($"\n{cost:C2} for {distance} km");

      Console.WriteLine("\nPress any key to continue...");
      Console.ReadKey();
    }

    Console.WriteLine("Bye!");
  }
}
