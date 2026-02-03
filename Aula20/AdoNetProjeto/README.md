# AdoNetProjeto

API mínima em .NET 8 usando ADO.NET + SQLite para CRUD de contatos.

## Pré-requisitos

- .NET SDK 8

## Passo a passo para rodar

1) Abra a pasta do projeto.

2) Restaure as dependências.

3) Inicie a aplicação.

Quando a aplicação subir, o banco SQLite será criado automaticamente no arquivo app.db na raiz do projeto.

## Como usar

### 1) Verificar saúde

- GET /health

Resposta esperada: { "status": "ok" }

### 2) Criar contato

- POST /contacts

Body (JSON):
- name (string)
- email (string)

### 3) Listar contatos

- GET /contacts

### 4) Buscar contato por id

- GET /contacts/{id}

### 5) Atualizar contato

- PUT /contacts/{id}

Body (JSON):
- name (string)
- email (string)

### 6) Remover contato

- DELETE /contacts/{id}

## Swagger

Em ambiente de desenvolvimento, o Swagger fica disponível na rota /swagger.

## Configuração do banco

A connection string está em appsettings.json:

ConnectionStrings:Default = Data Source=app.db

Se quiser alterar o local do banco, ajuste esse valor.

## Observações

- O email é único. Tentativas de duplicar retornam HTTP 409.
- Datas são salvas em UTC no formato ISO 8601.
