using System;
using System.ComponentModel;

namespace Animais
{
  public class Animal(string nome)
  {
    public string Nome { get; set; } = nome;

    public virtual void EmitirSom()
    {
      Console.WriteLine($"{Nome} está fazendo um som.");
    }
  }
  public class Cachorro(string nome, string raca) : Animal(nome)
  {
    public string Raca { get; set; } = raca;

    public override void EmitirSom()
    {
      Console.WriteLine($"{Nome} está latindo: Au au!");
    }
  }

  public class Gato(string nome, string raca) : Animal(nome)
  {
    public string Raca { get; set; } = raca;
    public override void EmitirSom()
    {
      base.EmitirSom();
      Console.WriteLine($"{Nome} está miando: miauuu!");
    }
  }
  public class AnimalGenerico(string nome, string especie, string som) : Animal(nome)
  {
    public string Especie { get; set; } = especie;

    public string Som { get; set; } = som;

    public override void EmitirSom()
    {
      Console.WriteLine($"{Nome} está fazendo: {Som}!");
    }
  }
}