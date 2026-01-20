namespace Aula11;

public class Livro
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicacao { get; set; }
    public string Editora { get; set; }
    public int NumeroPaginas { get; set; }
    public bool estaEmprestado { get; private set; }
    public string? emprestadoPara { get; private set; }
    public DateTime? DataEmprestimo { get; set; }

    public string Status
    {
        get { return estaEmprestado ? "Emprestado" : "Disponível"; }
    }

    public Livro()
    {
        Id = 0;
        Isbn = string.Empty;
        Titulo = string.Empty;
        Autor = string.Empty;
        Editora = string.Empty;
        estaEmprestado = false;
    }

    public Livro(int id, string titulo)
    {
        Id = id;
        Titulo = titulo;
        Isbn = string.Empty;
        Autor = string.Empty;
        Editora = string.Empty;
        estaEmprestado = false;
    }

    public Livro(string isbn, string titulo, string autor, int anoPublicacao, string editora, int numeroPaginas)
    {
        Id = 0;
        Isbn = isbn;
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = anoPublicacao;
        Editora = editora;
        NumeroPaginas = numeroPaginas;
        estaEmprestado = false;
    }

    public Livro(int id, string isbn, string titulo, string autor, int anoPublicacao, string editora, int numeroPaginas)
    {
        Id = id;
        Isbn = isbn;
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = anoPublicacao;
        Editora = editora;
        NumeroPaginas = numeroPaginas;
        estaEmprestado = false;
    }

    public bool Emprestar(string nomeUsuario)
    {
        if (estaEmprestado)
        {
            return false;
        }

        estaEmprestado = true;
        emprestadoPara = nomeUsuario;
        DataEmprestimo = DateTime.Now;
        return true;
    }

    public bool Devolver()
    {
        if (!estaEmprestado)
        {
            return false;
        }

        estaEmprestado = false;
        emprestadoPara = null;
        DataEmprestimo = null;
        return true;
    }

    public bool EstaDisponivel()
    {
        return !estaEmprestado;
    }

    public override string ToString()
    {
        string status = estaEmprestado 
            ? $"Emprestado para: {emprestadoPara} (desde {DataEmprestimo:dd/MM/yyyy})" 
            : "Disponível";

        return $"ISBN: {Isbn}\n" +
               $"Título: {Titulo}\n" +
               $"Autor: {Autor}\n" +
               $"Ano: {AnoPublicacao}\n" +
               $"Editora: {Editora}\n" +
               $"Páginas: {NumeroPaginas}\n" +
               $"Status: {status}";
    }

    public string ObterResumo()
    {
        return $"{Titulo} - {Autor} ({AnoPublicacao})";
    }
}
