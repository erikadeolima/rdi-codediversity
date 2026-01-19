using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Exemplo: Estruturas de Dados com FRUTAS ===\n");

        // NOVAS VARIÁVEIS (conforme instruções)
        int quantidadeFrutas = 3;
        double precoMedio = 2.50;
        bool estoqueDisponivel = true;
        string mensagem = "Bem-vindo ao programa de frutas!";

        // Impressão inicial das variáveis
        Console.WriteLine("=== ESTADO INICIAL DAS VARIÁVEIS ===");
        Console.WriteLine("Quantidade de frutas: " + quantidadeFrutas);
        Console.WriteLine("Preço médio: R$" + precoMedio);
        Console.WriteLine("Estoque disponível? " + estoqueDisponivel);
        Console.WriteLine(mensagem + "\n");

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
        frutasCores[1] = "Banana -> Amarela";
        frutasCores[2] = "Uva -> Roxa";
        Console.WriteLine("\nDicionário (Código -> Fruta+Cor):");
        foreach (var par in frutasCores)
        {
            Console.WriteLine($" - {par.Key}: {par.Value}");
        }

        Console.WriteLine("\n=== APLICAÇÃO DAS OPERAÇÕES ===");
        // Operações simples com cada tipo
        quantidadeFrutas += 2;                    // int: +2
        precoMedio *= 1.10;                       // double: +10%
        estoqueDisponivel = !estoqueDisponivel;   // bool: inverte
        mensagem += " Aproveite nossas ofertas!"; // string: concatena

        // Novos valores
        Console.WriteLine("\n=== NOVOS VALORES ===");
        Console.WriteLine("Nova quantidade: " + quantidadeFrutas);
        Console.WriteLine("Novo preço médio: R$" + precoMedio);
        Console.WriteLine("Novo estoque? " + estoqueDisponivel);
        Console.WriteLine("Nova mensagem: " + mensagem);

        Console.WriteLine("\nPor que estruturas são importantes?");
        Console.WriteLine("Organizam variáveis como quantidadeFrutas, precoMedio etc.");
        Console.WriteLine("Facilitam encontrar, atualizar e usar informações rapidamente.");
        
        Console.ReadKey();
    }
}
