# Guia Completo: Criar e Rodar Projeto C# do Zero

> **Observação importante:** Este guia foi escrito com base nas dificuldades reais encontradas durante a configuração inicial.

---

## 📋 Índice

1. [Pré-requisitos](#pré-requisitos)
2. [Criando o projeto](#1-criando-o-projeto)
3. [Estrutura de arquivos](#2-estrutura-de-arquivos)
4. [Rodando o projeto](#3-rodando-o-projeto)
5. [Resolvendo problemas comuns](#4-resolvendo-problemas-comuns)
6. [Comandos úteis](#5-comandos-úteis)
7. [Dicas para iniciantes](#6-dicas-para-iniciantes)

---

## ✅ Pré-requisitos

Antes de começar, verifique se você tem o .NET instalado:

```bash
dotnet --version
```

Se aparecer uma versão (ex: `8.0.x`), está pronto para começar!

---

## 1. Criando o Projeto

### Método 1: Usando o terminal (Recomendado)

```bash
# Criar o projeto (substitua "NomeDoProjeto" e "pasta_do_projeto")
dotnet new console -n NomeDoProjeto -o pasta_do_projeto

# Entrar na pasta
cd pasta_do_projeto

# Rodar o projeto
dotnet run
```

### Método 2: Criar manualmente (Se o comando falhar)

Se o `dotnet new` não funcionar como esperado, você pode criar os arquivos manualmente:

**Passo 1:** Crie uma pasta para o projeto

**Passo 2:** Crie o arquivo `.csproj` (arquivo de configuração):

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

**Passo 3:** Crie o arquivo `Program.cs` com seu código:

```csharp
using System;

namespace MeuProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Olá, Mundo!");
        }
    }
}
```

---

## 2. Estrutura de Arquivos

Após criar o projeto, você terá esta estrutura:

```
pasta_do_projeto/
├── MeuProjeto.csproj    ← Arquivo de configuração do projeto
├── Program.cs           ← Seu código principal
└── obj/                 ← Pasta temporária (pode ignorar)
```

---

## 3. Rodando o Projeto

### Comando principal:
```bash
dotnet run
```

### Outros comandos úteis:
```bash
dotnet build    # Apenas compila (não executa)
dotnet clean    # Limpa arquivos compilados
dotnet restore  # Restaura dependências
```

---

## 4. Resolvendo Problemas Comuns

### ⚠️ Problema: `dotnet new` não cria os arquivos

**Solução:** Crie os arquivos manualmente (veja Método 2 acima)

### ⚠️ Problema: O terminal não mostra a saída

**Solução:** 
1. Feche o terminal (Ctrl + `)
2. Abra um novo terminal
3. Execute `cd pasta_do_projeto && dotnet run`

### ⚠️ Problema: Warning CS8600 sobre valor nulo

**O que é:** 
```
warning CS8600: Conversão de literal nulo ou possível valor nulo em tipo não anulável.
```

**Por que acontece:**
- `Console.ReadLine()` pode retornar `null`
- Você declarou `string input` (tipo não anulável)
- O compilador avisa que isso *poderia* receber null

**Solução (opcional):**
```csharp
// Pode adicionar ? para permitir null
string? input = Console.ReadLine();
```

**Nota:** É só um aviso de segurança - seu código funciona!

### ⚠️ Problema: Erro de compilação

**Solução:** 
1. Verifique se está na pasta correta do projeto
2. Execute `dotnet clean` para limpar
3. Execute `dotnet restore` para restaurar dependências
4. Execute `dotnet run` novamente

---

## 5. Comandos Úteis

| Comando | Descrição |
|---------|-----------|
| `dotnet --version` | Verifica versão do .NET |
| `dotnet new console` | Cria nova Console Application |
| `dotnet run` | Compila e executa |
| `dotnet build` | Apenas compila |
| `dotnet clean` | Limpa arquivos compilados |
| `dotnet restore` | Restaura dependências |
| `dotnet --info` | Informações completas do .NET |

---

## 6. Dicas para Iniciantes

### 🔹 Onde escrever o código?
Edite o arquivo `Program.cs` - todo o seu código vai dentro do método `Main`.

### 🔹 Como fazer um "Olá, Mundo"?
```csharp
using System;

namespace MeuProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Olá, Mundo!");
        }
    }
}
```

### 🔹 Tipos de projetos C#:
| Tipo | Comando |
|------|---------|
| Console Application | `dotnet new console` |
| Class Library | `dotnet new classlib` |
| Web API | `dotnet new webapi` |
| MVC | `dotnet new mvc` |

### 🔹 Estrutura básica:
```csharp
using System;          // Importa funcionalidades

namespace NomeDoProjeto  // Organiza o código
{
    class Program       // Classe principal
    {
        static void Main(string[] args)  // Ponto de entrada
        {
            // Seu código aqui
        }
    }
}
```

---

## 📚 Recursos de Aprendizado

- [Documentação oficial do C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [Tutorial C# para iniciantes](https://docs.microsoft.com/pt-br/dotnet/csharp/tour-of-csharp/)
- [Dotnet CLI](https://docs.microsoft.com/pt-br/dotnet/core/tools/)

---

## ✅ Resumo Rápido

```bash
# 1. Verificar .NET
dotnet --version

# 2. Criar projeto
dotnet new console -n MeuApp -o minha_pasta

# 3. Entrar na pasta
cd minha_pasta

# 4. Editar Program.cs com seu código

# 5. Rodar
dotnet run
```

**Bom aprendizado!** 🎉

