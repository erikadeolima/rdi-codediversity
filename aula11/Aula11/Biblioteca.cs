namespace Aula11;

public class Biblioteca
{
    private Dictionary<string, Livro> livros;
    private Dictionary<string, Pessoa> usuarios;

    public Biblioteca()
    {
        livros = new Dictionary<string, Livro>();
        usuarios = new Dictionary<string, Pessoa>();
    }

    public int TotalLivros
    {
        get { return livros.Count; }
    }

    public int TotalUsuarios
    {
        get { return usuarios.Count; }
    }

    public bool CadastrarLivro(Livro livro)
    {
        if (livro == null || string.IsNullOrEmpty(livro.Isbn))
        {
            return false;
        }

        if (livros.ContainsKey(livro.Isbn))
        {
            return false;
        }

        livros[livro.Isbn] = livro;
        return true;
    }

    public bool CadastrarLivro(string isbn, string titulo, string autor, int anoPublicacao, string editora, int numeroPaginas)
    {
        Livro novoLivro = new Livro(isbn, titulo, autor, anoPublicacao, editora, numeroPaginas);
        return CadastrarLivro(novoLivro);
    }

    public bool CadastrarUsuario(Pessoa pessoa)
    {
        if (pessoa == null || string.IsNullOrEmpty(pessoa.Email))
        {
            return false;
        }

        if (usuarios.ContainsKey(pessoa.Email))
        {
            return false;
        }

        usuarios[pessoa.Email] = pessoa;
        return true;
    }

    public bool CadastrarUsuario(string nome, string email)
    {
        Pessoa novaPessoa = new Pessoa(nome, email);
        return CadastrarUsuario(novaPessoa);
    }

    public bool EmprestarLivro(string isbn, string emailUsuario)
    {
        if (!livros.ContainsKey(isbn))
        {
            return false;
        }

        if (!usuarios.ContainsKey(emailUsuario))
        {
            return false;
        }

        Livro livro = livros[isbn];
        Pessoa pessoa = usuarios[emailUsuario];

        return pessoa.PegarLivroEmprestado(livro);
    }

    public bool AtualizarUsuario(string email, string? novoNome = null, string? novoEmail = null)
    {
        if (!usuarios.ContainsKey(email))
        {
            return false;
        }

        Pessoa pessoa = usuarios[email];

        if (!string.IsNullOrEmpty(novoNome))
        {
            pessoa.Nome = novoNome;
        }

        if (!string.IsNullOrEmpty(novoEmail) && novoEmail != email)
        {
            if (usuarios.ContainsKey(novoEmail))
            {
                return false;
            }

            usuarios.Remove(email);
            pessoa.Email = novoEmail;
            usuarios[novoEmail] = pessoa;
        }

        return true;
    }

    public bool AtualizarLivro(string isbn, string? novoTitulo = null, string? novoAutor = null, 
                                int? novoAno = null, string? novaEditora = null, int? novoNumeroPaginas = null)
    {
        if (!livros.ContainsKey(isbn))
        {
            return false;
        }

        Livro livro = livros[isbn];

        if (!string.IsNullOrEmpty(novoTitulo))
        {
            livro.Titulo = novoTitulo;
        }

        if (!string.IsNullOrEmpty(novoAutor))
        {
            livro.Autor = novoAutor;
        }

        if (novoAno.HasValue)
        {
            livro.AnoPublicacao = novoAno.Value;
        }

        if (!string.IsNullOrEmpty(novaEditora))
        {
            livro.Editora = novaEditora;
        }

        if (novoNumeroPaginas.HasValue)
        {
            livro.NumeroPaginas = novoNumeroPaginas.Value;
        }

        return true;
    }

    public void ListarLivros()
    {
        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado na biblioteca.");
            return;
        }

        Console.WriteLine($"=== Livros Cadastrados ({livros.Count}) ===");
        int contador = 1;
        foreach (var livro in livros.Values)
        {
            string status = livro.EstaDisponivel() ? "Disponível" : "Emprestado";
            Console.WriteLine($"{contador}. {livro.ObterResumo()} - {status}");
            contador++;
        }
        Console.WriteLine();
    }

    public void ListarLivrosDetalhado()
    {
        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado na biblioteca.");
            return;
        }

        Console.WriteLine($"=== Livros Cadastrados - Detalhado ({livros.Count}) ===");
        int contador = 1;
        foreach (var livro in livros.Values)
        {
            Console.WriteLine($"--- Livro {contador} ---");
            Console.WriteLine(livro);
            Console.WriteLine();
            contador++;
        }
    }

    public void ListarUsuarios()
    {
        if (usuarios.Count == 0)
        {
            Console.WriteLine("Nenhum usuário cadastrado na biblioteca.");
            return;
        }

        Console.WriteLine($"=== Usuários Cadastrados ({usuarios.Count}) ===");
        int contador = 1;
        foreach (var pessoa in usuarios.Values)
        {
            Console.WriteLine($"{contador}. {pessoa.Nome} ({pessoa.Email})");
            Console.WriteLine($"   Livros emprestados: {pessoa.QuantidadeLivrosEmprestados}");
            contador++;
        }
        Console.WriteLine();
    }

    public List<Livro> BuscarLivros(string termoBusca)
    {
        List<Livro> resultados = new List<Livro>();

        if (string.IsNullOrEmpty(termoBusca))
        {
            return resultados;
        }

        termoBusca = termoBusca.ToLower();

        foreach (var livro in livros.Values)
        {
            bool encontrado = 
                livro.Titulo.ToLower().Contains(termoBusca) ||
                livro.Autor.ToLower().Contains(termoBusca) ||
                livro.Isbn.Contains(termoBusca) ||
                livro.Editora.ToLower().Contains(termoBusca);

            if (encontrado)
            {
                resultados.Add(livro);
            }
        }

        return resultados;
    }

    public Livro? BuscarLivroPorIsbn(string isbn)
    {
        if (livros.ContainsKey(isbn))
        {
            return livros[isbn];
        }
        return null;
    }

    public Pessoa? BuscarUsuarioPorEmail(string email)
    {
        if (usuarios.ContainsKey(email))
        {
            return usuarios[email];
        }
        return null;
    }

    public List<Livro> BuscarLivrosDisponiveis()
    {
        return livros.Values.Where(l => l.EstaDisponivel()).ToList();
    }

    public List<Livro> BuscarLivrosEmprestados()
    {
        return livros.Values.Where(l => !l.EstaDisponivel()).ToList();
    }

    public void ExibirResultadosBusca(List<Livro> resultados, string termoBusca)
    {
        if (resultados.Count == 0)
        {
            Console.WriteLine($"Nenhum livro encontrado para: '{termoBusca}'");
            return;
        }

        Console.WriteLine($"=== Resultados da Busca: '{termoBusca}' ({resultados.Count} encontrado(s)) ===");
        for (int i = 0; i < resultados.Count; i++)
        {
            var livro = resultados[i];
            string status = livro.EstaDisponivel() ? "Disponível" : "Emprestado";
            Console.WriteLine($"{i + 1}. {livro.ObterResumo()} - {status}");
        }
        Console.WriteLine();
    }
}
