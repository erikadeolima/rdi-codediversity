namespace Aula11;

public class Pessoa
{
    public string Nome { get; set; }
    public string Email { get; set; }
    private List<Livro> livrosEmprestados;
    private List<Livro> favoritos;

    public Pessoa(string nome, string email)
    {
        Nome = nome;
        Email = email;
        livrosEmprestados = new List<Livro>();
        favoritos = new List<Livro>();
    }

    public List<Livro> LivrosEmprestados
    {
        get { return new List<Livro>(livrosEmprestados); }
    }

    public List<Livro> Favoritos
    {
        get { return new List<Livro>(favoritos); }
    }

    public int QuantidadeLivrosEmprestados
    {
        get { return livrosEmprestados.Count; }
    }

    public int QuantidadeFavoritos
    {
        get { return favoritos.Count; }
    }

    public bool PegarLivroEmprestado(Livro livro)
    {
        if (livro == null)
        {
            return false;
        }

        if (!livro.EstaDisponivel())
        {
            return false;
        }

        if (livro.Emprestar(Nome))
        {
            livrosEmprestados.Add(livro);
            return true;
        }

        return false;
    }

    public bool DevolverLivro(Livro livro)
    {
        if (livro == null)
        {
            return false;
        }

        if (!livrosEmprestados.Contains(livro))
        {
            return false;
        }

        if (livro.Devolver())
        {
            livrosEmprestados.Remove(livro);
            return true;
        }

        return false;
    }

    public void ConsultarLivro(Livro livro)
    {
        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        Console.WriteLine("=== Informações do Livro ===");
        Console.WriteLine(livro);
        Console.WriteLine();

        if (livrosEmprestados.Contains(livro))
        {
            Console.WriteLine($"Você tem este livro emprestado desde {livro.DataEmprestimo:dd/MM/yyyy}");
        }
        else if (livro.EstaDisponivel())
        {
            Console.WriteLine("Este livro está disponível para empréstimo.");
        }
        else
        {
            Console.WriteLine($"Este livro está emprestado para: {livro.emprestadoPara}");
        }
    }

    public void AdicionarFavorito(Livro livro)
    {
        if (livro == null)
        {
            Console.WriteLine("Livro inválido.");
            return;
        }

        if (favoritos.Contains(livro))
        {
            Console.WriteLine($"'{livro.Titulo}' já está nos seus favoritos.");
            return;
        }

        favoritos.Add(livro);
        Console.WriteLine($"'{livro.Titulo}' adicionado aos favoritos!");
    }

    public bool RemoverFavorito(Livro livro)
    {
        if (livro == null)
        {
            return false;
        }

        return favoritos.Remove(livro);
    }

    public void ListarLivrosEmprestados()
    {
        if (livrosEmprestados.Count == 0)
        {
            Console.WriteLine("Você não tem livros emprestados no momento.");
            return;
        }

        Console.WriteLine($"=== Seus Livros Emprestados ({livrosEmprestados.Count}) ===");
        for (int i = 0; i < livrosEmprestados.Count; i++)
        {
            var livro = livrosEmprestados[i];
            Console.WriteLine($"{i + 1}. {livro.ObterResumo()}");
            Console.WriteLine($"   Emprestado desde: {livro.DataEmprestimo:dd/MM/yyyy}");
            Console.WriteLine();
        }
    }

    public void ListarFavoritos()
    {
        if (favoritos.Count == 0)
        {
            Console.WriteLine("Você não tem livros favoritos.");
            return;
        }

        Console.WriteLine($"=== Seus Favoritos ({favoritos.Count}) ===");
        for (int i = 0; i < favoritos.Count; i++)
        {
            var livro = favoritos[i];
            string status = livro.EstaDisponivel() ? "Disponível" : "Emprestado";
            Console.WriteLine($"{i + 1}. {livro.ObterResumo()} - {status}");
        }
        Console.WriteLine();
    }

    public override string ToString()
    {
        return $"Nome: {Nome}\n" +
               $"Email: {Email}\n" +
               $"Livros Emprestados: {livrosEmprestados.Count}\n" +
               $"Favoritos: {favoritos.Count}";
    }
}
