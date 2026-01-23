using System;
using System.Collections.Generic;
using Animais;

class Program
{
  static void Main(string[] args)
  {
    List<Animal> animais = new List<Animal>
    {
      new Cachorro("Thor", "Labrador"),
      new Cachorro("Rex", "Golden Retriever"),
      new Gato("Mimi", "Persa"),
      new Gato("Felix", "Siamês"),
      new AnimalGenerico("Dumbo", "Elefante", "Tromba"),
      new Animal("Ornintorrinco")
    };

    var exibidores = new Dictionary<Type, Action<Animal>>
    {
      { typeof(Cachorro), a => {
        var c = (Cachorro)a;
        Console.WriteLine($"Nome: {c.Nome}");
        Console.WriteLine($"Tipo: Cachorro");
        Console.WriteLine($"Raça: {c.Raca}");
      }},
      { typeof(Gato), a => {
        var g = (Gato)a;
        Console.WriteLine($"Nome: {g.Nome}");
        Console.WriteLine($"Tipo: Gato");
        Console.WriteLine($"Raça: {g.Raca}");
      }},
      { typeof(AnimalGenerico), a => {
        var ag = (AnimalGenerico)a;
        Console.WriteLine($"Nome: {ag.Nome}");
        Console.WriteLine($"Espécie: {ag.Especie}");
      }}
    };

    foreach (var animal in animais)
    {
      Console.WriteLine();

      var tipo = animal.GetType();
      if (exibidores.ContainsKey(tipo))
      {
        exibidores[tipo](animal);
        animal.EmitirSom();
        Console.WriteLine("-------------------");
        continue;
      }

      Console.WriteLine($"Nome: {animal.Nome}");
      Console.WriteLine($"Tipo: Animal genérico");
      animal.EmitirSom();
      Console.WriteLine("-------------------");
    }
  }
}