using System;
using Classes;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Teste Validador");

        Console.Write("Testando se a idade 20 é maior de idade: ");
        Console.WriteLine(Validador.EhMaiorDeIdade(20));
        Console.Write("Testando se o email teste@teste.com é válido: ");
        Console.WriteLine(Validador.EmailValido("teste@teste.com"));

        Pedido pedido = new Pedido();
        pedido.ValorTotal = 150.50m;
        Console.Write("Testando se o valor 150.50 é válido: ");
        Console.WriteLine(pedido.ValorValido(150.50m));

        // Teste SessaoUsuario - múltiplos logins
        SessaoUsuario sessao = new SessaoUsuario();
        sessao.DefinirUsuarioAtivo("João");
        Console.WriteLine("Logando com o usuário João ");
        Console.WriteLine($"Usuário logado: {sessao.NomeUsuarioAtivo}");

        sessao.DefinirUsuarioAtivo("Maria");
        Console.WriteLine("Logando com o usuário Maria ");
        Console.WriteLine($"Usuário logado: {sessao.NomeUsuarioAtivo}");

        sessao.DefinirUsuarioAtivo("Erika Lima");
        Console.WriteLine("Logando com o usuário Erika Lima ");
        Console.WriteLine($"Usuário logado: {sessao.NomeUsuarioAtivo}");
        Console.Write("Sessão ainda ativa? ");
        Console.WriteLine(sessao.UsuarioLogado());
    }
}
