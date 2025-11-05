## 🏫 **BibliotecaAPI**

Sistema de gerenciamento de empréstimos de livros — desenvolvido em **C# com ASP.NET Core Web API** e **Entity Framework Core (banco em memória)**.

Este projeto simula o funcionamento de uma biblioteca acadêmica, permitindo o **cadastro de livros e usuários**, **empréstimos e devoluções** com **cálculo automático de multas** e **relatórios gerenciais**.

---

## 🧩 **Descrição Geral**

A API permite:

* 📘 **Cadastrar livros**
* 👤 **Cadastrar usuários**
* 📚 **Registrar empréstimos**
* 📕 **Registrar devoluções**
* 💰 **Gerar multas automáticas por atraso**
* 📊 **Emitir relatórios (livros mais emprestados, usuários mais ativos, empréstimos atrasados)**

---

## ⚖️ **Regras de Negócio Implementadas**

| Regra                         | Descrição                                                                                  |
| ----------------------------- | ------------------------------------------------------------------------------------------ |
| **Limite de empréstimos**     | Cada usuário pode ter no máximo **3 empréstimos ativos**.                                  |
| **Status dos livros**         | Um livro pode estar `DISPONIVEL`, `EMPRESTADO` ou `RESERVADO`.                             |
| **Empréstimos**               | Livros **emprestados não podem ser reservados** ou emprestados novamente.                  |
| **Prazo por tipo de usuário** | Professores têm prazo de **15 dias**, alunos e funcionários têm **7 dias** para devolução. |
| **Multas**                    | R$ **1,00 por dia de atraso**. Geradas automaticamente na devolução.                       |
| **Bloqueio por multa**        | Usuários com multas pendentes não podem fazer novos empréstimos.                           |
| **Status de empréstimos**     | `ATIVO`, `FINALIZADO` ou `ATRASADO`, de acordo com o fluxo.                                |

---

## 🧱 **Diagrama Simplificado das Entidades**

```text
+-----------------+
|     LIVRO       |
+-----------------+
| ISBN (PK)       |
| Titulo          |
| Autor           |
| Categoria       |
| Status          |
| DataCadastro    |
+-----------------+
         |
         | 1..* 
         |
+-----------------+
|   EMPRESTIMO    |
+-----------------+
| Id (PK)         |
| ISBNLivro (FK)  |
| IdUsuario (FK)  |
| DataEmprestimo  |
| DataPrevista    |
| DataReal        |
| Status          |
+-----------------+
         |
         | 1..1
         |
+-----------------+
|     MULTA       |
+-----------------+
| Id (PK)         |
| IdEmprestimo(FK)|
| Valor           |
| Status          |
+-----------------+

+-----------------+
|    USUARIO      |
+-----------------+
| Id (PK)         |
| Nome            |
| Email           |
| Tipo            |
| DataCadastro    |
+-----------------+
```

---

## 🔌 **Endpoints Principais da API**

### 📘 **Livros**

| Método | Endpoint                            | Descrição                                                        | Exemplo de Corpo (JSON)                                                                               |
| ------ | ----------------------------------- | ---------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| `POST` | `/api/livro`                        | Cadastrar livro                                                  | `json { "isbn": "978-8535902772", "titulo": "O Hobbit", "autor": "J.R.R. Tolkien", "categoria": 0 } ` |
| `GET`  | `/api/livro`                        | Listar todos os livros                                           | —                                                                                                     |
| `PUT`  | `/api/livro/{isbn}/status?status=1` | Atualizar status (`0=DISPONIVEL`, `1=EMPRESTADO`, `2=RESERVADO`) | —                                                                                                     |

---

### 👤 **Usuários**

| Método | Endpoint                           | Descrição                             | Exemplo de Corpo (JSON)                                              |
| ------ | ---------------------------------- | ------------------------------------- | -------------------------------------------------------------------- |
| `POST` | `/api/usuario`                     | Cadastrar usuário                     | `json { "nome": "Ana Silva", "email": "ana@teste.com", "tipo": 0 } ` |
| `GET`  | `/api/usuario`                     | Listar usuários                       | —                                                                    |
| `GET`  | `/api/usuario/{id}/pode-emprestar` | Verificar se o usuário pode emprestar | —                                                                    |

---

### 📚 **Empréstimos**

| Método | Endpoint                        | Descrição                   | Exemplo de Corpo (JSON)                                   |
| ------ | ------------------------------- | --------------------------- | --------------------------------------------------------- |
| `POST` | `/api/emprestimo`               | Registrar empréstimo        | `json { "usuarioId": 1, "isbnLivro": "978-8535902772" } ` |
| `PUT`  | `/api/emprestimo/{id}/devolver` | Registrar devolução         | —                                                         |
| `GET`  | `/api/emprestimo`               | Listar todos os empréstimos | —                                                         |

---

### 📊 **Relatórios**

| Método | Endpoint                                 | Descrição                           |
| ------ | ---------------------------------------- | ----------------------------------- |
| `GET`  | `/api/relatorio/livros-mais-emprestados` | Lista livros mais emprestados       |
| `GET`  | `/api/relatorio/usuarios-mais-ativos`    | Lista usuários com mais empréstimos |
| `GET`  | `/api/relatorio/emprestimos-atrasados`   | Lista empréstimos atrasados         |

---

## 🧪 **Exemplos de Requisição e Resposta**

### 🔹 Cadastrar Livro

**POST** `/api/livro`

```json
{
  "isbn": "978-8535902772",
  "titulo": "O Hobbit",
  "autor": "J.R.R. Tolkien",
  "categoria": 0
}
```

**Resposta**

```json
"📘 Livro cadastrado com sucesso!"
```

---

### 🔹 Registrar Empréstimo

**POST** `/api/emprestimo`

```json
{
  "usuarioId": 1,
  "isbnLivro": "978-8535902772"
}
```

**Resposta**

```json
"📗 Empréstimo registrado com sucesso!"
```

---

### 🔹 Registrar Devolução

**PUT** `/api/emprestimo/1/devolver`
**Resposta**

```json
"📕 Devolução registrada com sucesso!"
```

---

## ⚙️ **Como Executar o Projeto**

### 🔧 Requisitos

* Visual Studio 2022 ou VS Code
* .NET 8 SDK (ou 7.0+)
* Git instalado

---

### ▶️ Passos de Execução

1. **Clonar o repositório**

   ```bash
   git clone https://github.com/seuusuario/BibliotecaAPI.git
   cd BibliotecaAPI
   ```

2. **Restaurar dependências**

   ```bash
   dotnet restore
   ```

3. **Executar o projeto**

   ```bash
   dotnet run
   ```

4. **Acessar a documentação Swagger**

   ```
   https://localhost:5001/swagger
   ```

---

🎓 **Autor:** Enzo Luciano Duarte e Ronaldo Kozan Junior
📅 **Data de entrega:** *05/11/2025*

---
