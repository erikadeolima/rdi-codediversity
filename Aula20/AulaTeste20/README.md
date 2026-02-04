# API de Contatos - PostgreSQL com Entity Framework Core

API minimal em .NET 8 usando **Entity Framework Core** com PostgreSQL.

## 🚀 Como Rodar

```bash
cd Aula20/AulaTeste20
dotnet run
```

A aplicação estará disponível em: **http://localhost:5264**

## 📖 Documentação (Swagger)

Acesse o Swagger UI para testar as rotas:

```
http://localhost:5264/swagger
```

## 📡 Endpoints

| Método | Rota             | Descrição                      |
| ------ | ---------------- | ------------------------------ |
| GET    | `/health`        | Verificar se a API está online |
| GET    | `/contacts`      | Listar todos os contatos       |
| GET    | `/contacts/{id}` | Buscar contato por ID          |
| POST   | `/contacts`      | Criar novo contato             |
| PUT    | `/contacts/{id}` | Atualizar contato              |
| DELETE | `/contacts/{id}` | Excluir contato                |

## 📦 Importar Coleções para Testes

### Postman

1. Abra o Postman
2. Clique em **Import**
3. Selecione o arquivo `postman-collection.json`
4. A coleção "Contacts API" será adicionada

### Insomnia

1. Abra o Insomnia
2. Clique em **Import**
3. Selecione o arquivo `insomnia-collection.json`
4. A coleção "Contacts API" será adicionada

### Thunder Client (VSCode)

1. Abra o Thunder Client no VSCode
2. Clique em **Import Collection**
3. Selecione o arquivo `thunder-collection.json`
4. A coleção "Contacts API" será adicionada

## 📝 Exemplos de Requisições

### 1. Verificar saúde da API

**cURL:**

```bash
curl -X GET http://localhost:5264/health
```

**Resposta:**

```json
{
  "status": "ok"
}
```

---

### 2. Criar contato

**cURL:**

```bash
curl -X POST http://localhost:5264/contacts \
  -H "Content-Type: application/json" \
  -d '{"name": "João Silva", "email": "joao@email.com"}'
```

**Resposta (201 Created):**

```json
{
  "id": 1
}
```

---

### 3. Listar todos os contatos

**cURL:**

```bash
curl -X GET http://localhost:5264/contacts
```

**Resposta (200 OK):**

```json
[
  {
    "id": 1,
    "name": "João Silva",
    "email": "joao@email.com",
    "createdAt": "2024-01-15T10:30:00Z"
  }
]
```

---

### 4. Buscar contato por ID

**cURL:**

```bash
curl -X GET http://localhost:5264/contacts/1
```

**Resposta (200 OK):**

```json
{
  "id": 1,
  "name": "João Silva",
  "email": "joao@email.com",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

---

### 5. Atualizar contato

**cURL:**

```bash
curl -X PUT http://localhost:5264/contacts/1 \
  -H "Content-Type: application/json" \
  -d '{"name": "João Santos", "email": "joao.santos@email.com"}'
```

**Resposta (204 No Content)**

---

### 6. Excluir contato

**cURL:**

```bash
curl -X DELETE http://localhost:5264/contacts/1
```

**Resposta (204 No Content)**

---

## 📊 Códigos de Status HTTP

| Código | Significado                                        |
| ------ | -------------------------------------------------- |
| 200    | OK - Requisição bem-sucedida                       |
| 201    | Created - Recurso criado com sucesso               |
| 204    | No Content - Operação bem-sucedida                 |
| 400    | Bad Request - Dados inválidos                      |
| 404    | Not Found - Recurso não encontrado                 |
| 409    | Conflict - Violação de restrição (email duplicado) |

## 🔧 Configuração do Banco de Dados

Configure a conexão no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Database=test20;Username=postgres;Password=postgres"
  }
}
```

## ⚠️ Observação

Para usar os endpoints de criação, atualização e exclusão, é necessário:

1. PostgreSQL rodando
2. Banco de dados `test20` criado
3. A tabela `Contacts` será criada automaticamente pelo EF Core

**Nota:** Esta versão usa **Entity Framework Core** (não ADO.NET manual). O EF Core gerencia a criação automática da tabela se ela não existir.

## 📁 Estrutura do Projeto

```
AulaTeste20/
├── Data/
│   └── AppDbContext.cs    # DbContext do EF Core
├── Models/
│   └── Contact.cs         # Modelo de dados
├── Contracts/
│   ├── ContactCreateRequest.cs
│   └── ContactUpdateRequest.cs
├── Program.cs              # Endpoints da API
├── appsettings.json        # Configurações
└── README.md
```

## 🛠️ Tecnologias

- .NET 8
- Entity Framework Core 9.0
- Npgsql.EntityFrameworkCore.PostgreSQL 9.0
- Swagger / Swashbuckle
- Minimal APIs
