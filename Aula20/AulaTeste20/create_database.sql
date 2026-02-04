-- ============================================
-- SCRIPT ÚNICO PARA API DE CONTATOS
-- Execute este script inteiro no DBeaver
-- ============================================

-- 1. CRIAR BANCO DE DADOS (se não existir)
DO $$
BEGIN
    IF NOT EXISTS (SELECT FROM pg_database WHERE datname = 'test20') THEN
        CREATE DATABASE test20;
    END IF;
END $$;

-- 2. CONECTAR AO BANCO
--    No DBeaver: Clique com botão direito no banco "test20" 
--    e selecione "Set as default" (Definir como padrão)

-- 3. CRIAR TABELA
CREATE TABLE IF NOT EXISTS Contacts (
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW()
);

-- 4. VERIFICAR CRIAÇÃO
SELECT 'Tabela Contacts criada com sucesso!' AS status;
SELECT * FROM Contacts;

-- FIM DO SCRIPT
-- ============================================

-- INSTRUÇÕES PARA DBEAVER:
-- 1. Conecte-se ao servidor PostgreSQL no DBeaver
-- 2. Execute as linhas 1-10 (criação do banco)
-- 3. Clique com botão direito em "Databases" → "Refresh"
-- 4. Selecione o banco "test20" e clique em "Set as default"
-- 5. Execute as linhas 15-21 (criação da tabela)
-- 6. Para ver a tabela: clique em "Tables" → "Contacts" → "Data"
