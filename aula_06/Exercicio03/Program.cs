using System;
using System.Collections.Generic;

namespace DemoTiposDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Crie uma lista de inteiros chamada numeros
            List<int> numeros = new List<int>();

            // 2. Adicione três números à lista: 10, 20, 30
            numeros.Add(10);
            numeros.Add(20);
            numeros.Add(30);

            // 3. Imprima todos os números usando um foreach
            Console.WriteLine("Lista Inicial:");
            foreach (int n in numeros)
            {
                Console.WriteLine(n);
            }

            // 4. Atualize o número na posição 1 (índice 1) para 50
            numeros[1] = 50;
            Console.WriteLine("\nLista Atualizada:");
            foreach (int n in numeros)
            {
                Console.WriteLine(n);
            }

            // 5. Atualize o número 10 para 5 utilizando indexOf para achar o índice
            int indiceDoDez = numeros.IndexOf(10);
            if (indiceDoDez != -1)
            {
                numeros[indiceDoDez] = 5;
            }

            Console.WriteLine("\nLista Atualizada:");
            foreach (int n in numeros)
            {
                Console.WriteLine(n);
            }

            // 6. Remova o número 30 da lista
            numeros.Remove(30);

            // 7. Imprima novamente os números atualizados
            Console.WriteLine("\nLista Atualizada:");
            foreach (int n in numeros)
            {
                Console.WriteLine(n);
            }

            // 8. Mostre na tela a quantidade de itens na lista (Count)
            Console.WriteLine($"\nQuantidade de itens na lista: {numeros.Count}");
        }
    }
}