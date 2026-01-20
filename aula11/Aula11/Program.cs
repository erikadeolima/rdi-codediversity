using Aula11;

Biblioteca biblioteca = new Biblioteca();

Pessoa? SelecionarUsuario(Biblioteca biblioteca, string trecho)
{
    List<Pessoa> usuariosEncontrados = biblioteca.BuscarPessoasPorTrecho(trecho);

    if (usuariosEncontrados.Count == 0)
    {
        return null;
    }

    if (usuariosEncontrados.Count == 1)
    {
        return usuariosEncontrados[0];
    }

    Console.WriteLine($"\nForam encontrados {usuariosEncontrados.Count} usuário(s) com '{trecho}':");
    for (int i = 0; i < usuariosEncontrados.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {usuariosEncontrados[i].Nome} ({usuariosEncontrados[i].Email})");
    }

    Console.Write("\nDigite o número do usuário desejado: ");
    string? entrada = Console.ReadLine();

    if (int.TryParse(entrada, out int escolha) && escolha >= 1 && escolha <= usuariosEncontrados.Count)
    {
        return usuariosEncontrados[escolha - 1];
    }

    Console.WriteLine("Seleção inválida!");
    return null;
}

Console.WriteLine("Bem vindo a Biblioteca Ponei!");
Console.WriteLine();

while (true)
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Listar todos os livros");
    Console.WriteLine("2 - Emprestar livro");
    Console.WriteLine("3 - Devolver livro");
    Console.WriteLine("4 - Cadastrar livro");
    Console.WriteLine("5 - Cadastrar usuário");
    Console.WriteLine("6 - Listar usuários");
    Console.WriteLine("7 - Sair");
    Console.Write("\nOpção: ");

    string? entrada = Console.ReadLine();

    if (string.IsNullOrEmpty(entrada))
    {
        Console.WriteLine("Opção inválida. Tente novamente.\n");
        continue;
    }

    if (!int.TryParse(entrada, out int opcaoEscolhida))
    {
        Console.WriteLine("Opção inválida. Digite um número.\n");
        continue;
    }

    Console.WriteLine();

    switch (opcaoEscolhida)
    {
        case 1:
            Console.WriteLine("=== Livros Cadastrados ===");
            biblioteca.ListarLivros();
            Console.WriteLine();
            break;

        case 2:
            Console.WriteLine("=== Emprestar Livro ===");
            biblioteca.ListarLivros();
            Console.Write("\nDigite o ID do livro: ");
            
            if (int.TryParse(Console.ReadLine(), out int idLivro))
            {
                Livro? livro = biblioteca.BuscarLivroPorId(idLivro);
                
                if (livro == null)
                {
                    Console.WriteLine("Livro não encontrado!\n");
                    break;
                }

                if (!livro.EstaDisponivel())
                {
                    Console.WriteLine($"O livro '{livro.Titulo}' já está emprestado!\n");
                    break;
                }

                Pessoa? pessoa = null;
                bool emprestimoRealizado = false;

                while (!emprestimoRealizado)
                {
                    Console.Write("Digite o nome ou trecho do nome do usuário (ou 'cancelar' para voltar): ");
                    string? nomeUsuario = Console.ReadLine();

                    if (string.IsNullOrEmpty(nomeUsuario))
                    {
                        Console.WriteLine("Nome inválido!\n");
                        continue;
                    }

                    if (nomeUsuario.ToLower() == "cancelar")
                    {
                        Console.WriteLine("Operação cancelada.\n");
                        break;
                    }

                    pessoa = SelecionarUsuario(biblioteca, nomeUsuario);

                    if (pessoa == null)
                    {
                        Console.WriteLine($"\nNenhum usuário encontrado com '{nomeUsuario}'. Deseja cadastrar? (s/n): ");
                        string? resposta = Console.ReadLine();
                        
                        if (resposta?.ToLower() == "s")
                        {
                            Console.Write("Digite o email: ");
                            string? email = Console.ReadLine();
                            
                            if (string.IsNullOrEmpty(email))
                            {
                                Console.WriteLine("Email inválido!\n");
                                continue;
                            }

                            pessoa = new Pessoa(nomeUsuario, email);
                            biblioteca.CadastrarUsuario(pessoa);
                            Console.WriteLine($"Usuário '{nomeUsuario}' cadastrado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Tente buscar novamente ou digite 'cancelar' para voltar.\n");
                            continue;
                        }
                    }

                    if (pessoa != null)
                    {
                        biblioteca.EmprestarLivros(livro, pessoa);
                        Console.WriteLine($"Livro '{livro.Titulo}' emprestado para {pessoa.Nome}!\n");
                        emprestimoRealizado = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("ID inválido!\n");
            }
            break;

        case 3:
            Console.WriteLine("=== Devolver Livro ===");
            Pessoa? pessoaDevolver = null;
            bool devolucaoRealizada = false;

            while (!devolucaoRealizada)
            {
                Console.Write("Digite o nome ou trecho do nome do usuário (ou 'cancelar' para voltar): ");
                string? nomeDevolver = Console.ReadLine();

                if (string.IsNullOrEmpty(nomeDevolver))
                {
                    Console.WriteLine("Nome inválido!\n");
                    continue;
                }

                if (nomeDevolver.ToLower() == "cancelar")
                {
                    Console.WriteLine("Operação cancelada.\n");
                    break;
                }

                pessoaDevolver = SelecionarUsuario(biblioteca, nomeDevolver);

                if (pessoaDevolver == null)
                {
                    Console.WriteLine("Usuário não encontrado! Tente buscar novamente ou digite 'cancelar' para voltar.\n");
                    continue;
                }

                if (pessoaDevolver.QuantidadeLivrosEmprestados == 0)
                {
                    Console.WriteLine("Usuário não tem livros emprestados!\n");
                    break;
                }

                Console.WriteLine("\nLivros emprestados:");
                pessoaDevolver.ListarLivrosEmprestados();
                Console.Write("Digite o ID do livro para devolver: ");

                if (int.TryParse(Console.ReadLine(), out int idDevolver))
                {
                    Livro? livroDevolver = biblioteca.BuscarLivroPorId(idDevolver);

                    if (livroDevolver == null)
                    {
                        Console.WriteLine("Livro não encontrado!\n");
                        break;
                    }

                    biblioteca.DevolverLivro(livroDevolver, pessoaDevolver);
                    Console.WriteLine();
                    devolucaoRealizada = true;
                }
                else
                {
                    Console.WriteLine("ID inválido!\n");
                    break;
                }
            }
            break;

        case 4:
            Console.WriteLine("=== Cadastrar Livro ===");
            Console.Write("Digite o ID do livro: ");
            
            if (int.TryParse(Console.ReadLine(), out int novoId))
            {
                Console.Write("Digite o título do livro: ");
                string? novoTitulo = Console.ReadLine();

                if (string.IsNullOrEmpty(novoTitulo))
                {
                    Console.WriteLine("Título inválido!\n");
                    break;
                }

                Livro novoLivro = new Livro(novoId, novoTitulo);
                biblioteca.CadastrarLivro(novoLivro);
                Console.WriteLine($"Livro '{novoTitulo}' cadastrado com sucesso!\n");
            }
            else
            {
                Console.WriteLine("ID inválido!\n");
            }
            break;

        case 5:
            Console.WriteLine("=== Cadastrar Usuário ===");
            Console.Write("Digite o nome: ");
            string? novoNome = Console.ReadLine();
            
            if (string.IsNullOrEmpty(novoNome))
            {
                Console.WriteLine("Nome inválido!\n");
                break;
            }

            Console.Write("Digite o email: ");
            string? novoEmail = Console.ReadLine();

            if (string.IsNullOrEmpty(novoEmail))
            {
                Console.WriteLine("Email inválido!\n");
                break;
            }

            Pessoa novoUsuario = new Pessoa(novoNome, novoEmail);
            biblioteca.CadastrarUsuario(novoUsuario);
            Console.WriteLine($"Usuário '{novoNome}' cadastrado com sucesso!\n");
            break;

        case 6:
            Console.WriteLine("=== Usuários Cadastrados ===");
            if (biblioteca.Pessoa.Count == 0)
            {
                Console.WriteLine("Nenhum usuário cadastrado.\n");
            }
            else
            {
                foreach (var pessoa in biblioteca.Pessoa)
                {
                    Console.WriteLine($"- {pessoa.Nome} ({pessoa.Email}) - {pessoa.QuantidadeLivrosEmprestados} livro(s) emprestado(s)");
                }
                Console.WriteLine();
            }
            break;

        case 7:
            Console.WriteLine("Obrigado por usar a Biblioteca Ponei! Até logo!");
            return;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.\n");
            break;
    }
}
