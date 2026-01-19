/* 
1 - Crie um array de string com 3 posições. 2 - Preencha o array com 3 frutas (por exemplo: Banana, Maçã, Uva). 3 - Imprima todas as frutas usando um laço for. 4 - Atualize a fruta na posição 1 para "Pera". 5 - Reimprima o array atualizado.


 */
using System;

class Program
{
    static void Main()
    {
        // 1 e 2 - Criando e preenchendo o array com 3 frutas
        string[] frutas = new string[3] { "Banana", "Maçã", "Uva" };

        // 3 - Imprimindo todas as frutas usando um laço for
        Console.WriteLine("Lista inicial de frutas:");
        for (int i = 0; i < frutas.Length; i++)
        {
            Console.WriteLine($"Posição {i}: {frutas[i]}");
        }

        // 4 - Atualizando a fruta na posição 1 para "Pera"
        // Lembre-se: em C#, arrays começam no índice 0. A posição 1 é o segundo item.
        frutas[1] = "Pera";

        Console.WriteLine("\n--- Atualizando a posição 1 para 'Pera' ---\n");

        // 5 - Reimprimindo o array atualizado
        Console.WriteLine("Lista atualizada de frutas:");
        for (int i = 0; i < frutas.Length; i++)
        {
            Console.WriteLine($"Posição {i}: {frutas[i]}");
        }
    }
}