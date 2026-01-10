using System;

namespace TabuadaLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuarPrograma = true;

            while (continuarPrograma)
            {
                int numero;
                bool numeroValido = false;

                while (!numeroValido)
                {
                    Console.Write("Digite um número para ver sua tabuada (1-10): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out numero) && numero >= 1 && numero <= 10)
                    {
                        numeroValido = true;

                        Console.WriteLine($"\n=== TABUADA DO {numero} ===");
                        for (int i = 1; i <= 10; i++)
                        {
                            Console.WriteLine($"{numero} x {i} = {numero * i}");
                        }
                        Console.WriteLine("========================\n");
                    }
                    else
                    {
                        Console.WriteLine("ERRO: Digite um NÚMERO INTEIRO entre 1 e 10!");
                    }
                }

                Console.Write("Digite 'sair' para encerrar ou qualquer tecla para uma nova tabuada: ");
                string escolha = Console.ReadLine()?.Trim().ToLower();

                if (escolha == "sair")
                {
                    Console.WriteLine("Saindo...");
                    continuarPrograma = false;
                }
                else
                {
                    Console.WriteLine("Nova tabuada...\n");
                }
            }
        }
    }
}
