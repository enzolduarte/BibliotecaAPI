using BibliotecaAPI.Data;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public class EmprestimoService
    {
        private readonly BibliotecaContext _context;

        public EmprestimoService(BibliotecaContext context)
        {
            _context = context;
        }

        public void RegistrarEmprestimo(int userId, string isbn)
        {
            var usuario = _context.Usuarios.Find(userId);
            var livro = _context.Livros.Find(isbn);

            if (livro == null || livro.Status != StatusLivro.DISPONIVEL)
                throw new Exception("Livro indisponível.");

            int emprestimosAtivos = _context.Emprestimos.Count(e => e.IdUsuario == userId && e.Status == StatusEmprestimo.ATIVO);
            if (emprestimosAtivos >= 3)
                throw new Exception("Usuário atingiu o limite de empréstimos.");

            int dias = usuario.Tipo == TipoUsuario.PROFESSOR ? 15 : 7;
            var emprestimo = new Emprestimo
            {
                IdUsuario = userId,
                ISBNLivro = isbn,
                DataPrevistaDevolucao = DateTime.Now.AddDays(dias)
            };

            livro.Status = StatusLivro.EMPRESTADO;
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();
        }

        public void RegistrarDevolucao(int emprestimoId)
        {
            var emp = _context.Emprestimos.Find(emprestimoId);
            if (emp == null || emp.Status != StatusEmprestimo.ATIVO)
                throw new Exception("Empréstimo não encontrado ou já finalizado.");

            emp.DataRealDevolucao = DateTime.Now;

            if (emp.DataRealDevolucao > emp.DataPrevistaDevolucao)
            {
                var diasAtraso = (emp.DataRealDevolucao.Value - emp.DataPrevistaDevolucao).Days;
                _context.Multas.Add(new Multa
                {
                    IdEmprestimo = emp.Id,
                    Valor = diasAtraso * 1.0m
                });
                emp.Status = StatusEmprestimo.ATRASADO;
            }
            else emp.Status = StatusEmprestimo.FINALIZADO;

            var livro = _context.Livros.Find(emp.ISBNLivro);
            livro.Status = StatusLivro.DISPONIVEL;

            _context.SaveChanges();
        }

        public List<Emprestimo> ListarEmprestimos()
        {
            return _context.Emprestimos.ToList();
        }

    }
}
