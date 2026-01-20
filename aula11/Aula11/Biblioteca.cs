namespace Aula11;

public class Biblioteca
{
    public List<Livro> Livros { get; set; } = new List<Livro>();
    public List<Pessoa> Pessoa { get; set; } = new List<Pessoa>();

    public Biblioteca()
    {
        Livro aCabecaDoSanto = new Livro(1, "A Cabeça do Santo");
        Livro it = new Livro(2, "IT: A Coisa");

        CadastrarLivro(aCabecaDoSanto);
        CadastrarLivro(it);
    }

    public void CadastrarLivro(Livro livro)
    {
        Livros.Add(livro);
    }

    public void CadastrarUsuario(Pessoa usuario)
    {
        Pessoa.Add(usuario);
    }

    public void EmprestarLivros(Livro livro, Pessoa pessoa)
    {
        pessoa.PegarLivroEmprestado(livro);
    }

    public void ListarLivros()
    {
        foreach (Livro livro in Livros)
        {
            Console.WriteLine($"ID: {livro.Id} - Título: {livro.Titulo} - Status: {livro.Status}");
        }
    }

    public Livro? BuscarLivroPorId(int id)
    {
        return Livros.FirstOrDefault(l => l.Id == id);
    }

    public Pessoa? BuscarPessoaPorNome(string nome)
    {
        return Pessoa.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public List<Pessoa> BuscarPessoasPorTrecho(string trecho)
    {
        if (string.IsNullOrEmpty(trecho))
        {
            return new List<Pessoa>();
        }

        return Pessoa.Where(p => p.Nome.Contains(trecho, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void DevolverLivro(Livro livro, Pessoa pessoa)
    {
        if (livro.Devolver())
        {
            pessoa.DevolverLivro(livro);
            Console.WriteLine($"Livro '{livro.Titulo}' devolvido com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao devolver o livro.");
        }
    }
}
