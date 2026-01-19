using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Exemplo: Estruturas de Dados ===\n");

        // Imagine que você precisa guardar informações sobre frutas
        // Sem estrutura: seria difícil organizar e acessar
        // Com estrutura: fica fácil!

        // Tipo simples (string)
        string fruta = "Banana";
        Console.WriteLine($"Uma fruta: {fruta}");

        // Array: lista fixa de frutas
        string[] frutasArray = { "Banana", "Maçã", "Uva" };
        Console.WriteLine("\nArray de frutas:");
        foreach (var f in frutasArray)
        {
            Console.WriteLine($" - {f}");
        }

        // Lista: coleção dinâmica
        List<string> frutasLista = new List<string>();
        frutasLista.Add("Laranja");
        frutasLista.Add("Melancia");
        Console.WriteLine("\nLista de frutas (dinâmica):");
        foreach (var f in frutasLista)
        {
            Console.WriteLine($" - {f}");
        }

        // Dicionário: chave-valor (fruta -> cor)
        Dictionary<int, string> frutasCores = new Dictionary<int, string>();
        frutasCores[1] = "Banana";
        frutasCores[2] = "Uva";
        Console.WriteLine("\nDicionário (Chave -> Valor):");
        foreach (var par in frutasCores)
        {
            Console.WriteLine($" - {par.Key} é {par.Value}");
        }

        Console.WriteLine("\nPor que isso é importante?");
        Console.WriteLine("Organizar dados facilita encontrar, atualizar e usar informações rapidamente.");
    }
}