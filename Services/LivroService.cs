using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class LivroService
    {
        private readonly BibliotecaContext _context;

        public LivroService(BibliotecaContext context)
        {
            _context = context;
        }

        // ✅ Cadastrar novo livro
        public void CadastrarLivro(Livro livro)
        {
            // Evita cadastrar o mesmo ISBN duas vezes
            if (_context.Livros.Any(l => l.ISBN == livro.ISBN))
                throw new Exception("Já existe um livro cadastrado com esse ISBN.");

            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        // ✅ Atualizar status (ex: DISPONIVEL → EMPRESTADO)
        public void AtualizarStatus(string isbn, StatusLivro novoStatus)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.ISBN == isbn);
            if (livro == null)
                throw new Exception("Livro não encontrado.");

            livro.Status = novoStatus;
            _context.SaveChanges();
        }

        // ✅ Verifica se um livro está disponível
        public bool LivroDisponivel(string isbn)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.ISBN == isbn);
            return livro != null && livro.Status == StatusLivro.DISPONIVEL;
        }

        // ✅ Retorna todos os livros
        public List<Livro> ListarLivros()
        {
            return _context.Livros.AsNoTracking().ToList();
        }
    }
}

