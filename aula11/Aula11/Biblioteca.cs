namespace Aula11;

public class Biblioteca
{
    public List<Livro> Livros { get; set; } = new List<Livro>();
    public List<Pessoa> Pessoa { get; set; } = new List<Pessoa>();

    public Biblioteca()
    {
        CadastrarLivro(new Livro(1, "9788535914849", "1984", "George Orwell", 2021, "Companhia das Letras", 416));
    CadastrarLivro(new Livro(2, "9788535933659", "Torto Arado", "Itamar Vieira Junior", 2019, "Todavia", 264));
    CadastrarLivro(new Livro(3, "9788501115164", "Quarto de Despejo", "Carolina Maria de Jesus", 2014, "Ática", 200));
    CadastrarLivro(new Livro(4, "9788535910698", "A Hora da Estrela", "Clarice Lispector", 2017, "Rocco", 88));
    CadastrarLivro(new Livro(5, "9788544001714", "Memórias Póstumas de Brás Cubas", "Machado de Assis", 2023, "Antofágica", 448));
    CadastrarLivro(new Livro(6, "9788595081536", "O Alquimista", "Paulo Coelho", 2017, "Paralela", 208));
    CadastrarLivro(new Livro(7, "9788576865247", "A Psicologia Financeira", "Morgan Housel", 2021, "HarperCollins", 304));
    CadastrarLivro(new Livro(8, "9786555355437", "Hábitos Atômicos", "James Clear", 2019, "Alta Life", 320));
    CadastrarLivro(new Livro(9, "9788535925821", "O Sol é para Todos", "Harper Lee", 2015, "José Olympio", 350));
    CadastrarLivro(new Livro(10, "9788532531384", "O Pequeno Príncipe", "Antoine de Saint-Exupéry", 2018, "HarperCollins", 96));
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
