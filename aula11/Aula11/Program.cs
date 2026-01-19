using Aula11;

Console.WriteLine("=== Sistema de Biblioteca ===\n");

Biblioteca biblioteca = new Biblioteca();

Console.WriteLine("=== 1. Cadastrar Livros ===");
biblioteca.CadastrarLivro("978-85-359-0277-5", "Dom Casmurro", "Machado de Assis", 1899, "Editora Globo", 256);
biblioteca.CadastrarLivro("978-85-325-1234-6", "O Cortiço", "Aluísio Azevedo", 1890, "Editora Ática", 320);
biblioteca.CadastrarLivro("978-85-250-1234-7", "Memórias Póstumas de Brás Cubas", "Machado de Assis", 1881, "Editora Globo", 288);
biblioteca.CadastrarLivro("978-85-200-5678-9", "O Guarani", "José de Alencar", 1857, "Editora Saraiva", 400);
Console.WriteLine($"Total de livros cadastrados: {biblioteca.TotalLivros}\n");

Console.WriteLine("=== 2. Cadastrar Usuários ===");
biblioteca.CadastrarUsuario("João Silva", "joao.silva@email.com");
biblioteca.CadastrarUsuario("Maria Santos", "maria.santos@email.com");
biblioteca.CadastrarUsuario("Pedro Oliveira", "pedro.oliveira@email.com");
Console.WriteLine($"Total de usuários cadastrados: {biblioteca.TotalUsuarios}\n");

Console.WriteLine("=== 3. Listar Livros ===");
biblioteca.ListarLivros();

Console.WriteLine("=== 4. Listar Usuários ===");
biblioteca.ListarUsuarios();

Console.WriteLine("=== 5. Emprestar Livros ===");
if (biblioteca.EmprestarLivro("978-85-359-0277-5", "joao.silva@email.com"))
{
    Console.WriteLine("Livro 'Dom Casmurro' emprestado para João Silva!");
}
if (biblioteca.EmprestarLivro("978-85-325-1234-6", "joao.silva@email.com"))
{
    Console.WriteLine("Livro 'O Cortiço' emprestado para João Silva!");
}
if (biblioteca.EmprestarLivro("978-85-250-1234-7", "maria.santos@email.com"))
{
    Console.WriteLine("Livro 'Memórias Póstumas de Brás Cubas' emprestado para Maria Santos!");
}
Console.WriteLine();

Console.WriteLine("=== 6. Listar Livros (após empréstimos) ===");
biblioteca.ListarLivros();

Console.WriteLine("=== 7. Buscar Livros ===");
List<Livro> resultados1 = biblioteca.BuscarLivros("Machado");
biblioteca.ExibirResultadosBusca(resultados1, "Machado");

List<Livro> resultados2 = biblioteca.BuscarLivros("Cortiço");
biblioteca.ExibirResultadosBusca(resultados2, "Cortiço");

List<Livro> resultados3 = biblioteca.BuscarLivros("Globo");
biblioteca.ExibirResultadosBusca(resultados3, "Globo");

Console.WriteLine("=== 8. Buscar Livro por ISBN ===");
Livro? livroEncontrado = biblioteca.BuscarLivroPorIsbn("978-85-359-0277-5");
if (livroEncontrado != null)
{
    Console.WriteLine("Livro encontrado:");
    Console.WriteLine(livroEncontrado);
    Console.WriteLine();
}

Console.WriteLine("=== 9. Buscar Livros Disponíveis ===");
List<Livro> disponiveis = biblioteca.BuscarLivrosDisponiveis();
Console.WriteLine($"Livros disponíveis: {disponiveis.Count}");
foreach (var livro in disponiveis)
{
    Console.WriteLine($"- {livro.ObterResumo()}");
}
Console.WriteLine();

Console.WriteLine("=== 10. Buscar Livros Emprestados ===");
List<Livro> emprestados = biblioteca.BuscarLivrosEmprestados();
Console.WriteLine($"Livros emprestados: {emprestados.Count}");
foreach (var livro in emprestados)
{
    Console.WriteLine($"- {livro.ObterResumo()}");
}
Console.WriteLine();

Console.WriteLine("=== 11. Atualizar Livro ===");
if (biblioteca.AtualizarLivro("978-85-359-0277-5", novoTitulo: "Dom Casmurro - Edição Especial", novoAno: 1900))
{
    Console.WriteLine("Livro atualizado com sucesso!");
    livroEncontrado = biblioteca.BuscarLivroPorIsbn("978-85-359-0277-5");
    if (livroEncontrado != null)
    {
        Console.WriteLine($"Novo título: {livroEncontrado.Titulo}");
        Console.WriteLine($"Novo ano: {livroEncontrado.AnoPublicacao}");
    }
}
Console.WriteLine();

Console.WriteLine("=== 12. Atualizar Usuário ===");
if (biblioteca.AtualizarUsuario("joao.silva@email.com", novoNome: "João Silva Santos"))
{
    Console.WriteLine("Usuário atualizado com sucesso!");
    Pessoa? usuario = biblioteca.BuscarUsuarioPorEmail("joao.silva@email.com");
    if (usuario != null)
    {
        Console.WriteLine($"Novo nome: {usuario.Nome}");
    }
}
Console.WriteLine();

Console.WriteLine("=== 13. Listar Livros Detalhado ===");
biblioteca.ListarLivrosDetalhado();

Console.WriteLine("=== 14. Informações Finais ===");
Console.WriteLine($"Total de livros: {biblioteca.TotalLivros}");
Console.WriteLine($"Total de usuários: {biblioteca.TotalUsuarios}");
