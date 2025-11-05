using BibliotecaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public RelatorioController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/relatorio/livros-mais-emprestados
        [HttpGet("livros-mais-emprestados")]
        public IActionResult LivrosMaisEmprestados()
        {
            var result = _context.Emprestimos
                .GroupBy(e => e.ISBNLivro)
                .Select(g => new { ISBN = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .ToList();

            return Ok(result);
        }

        // GET: api/relatorio/usuarios-mais-ativos
        [HttpGet("usuarios-mais-ativos")]
        public IActionResult UsuariosMaisAtivos()
        {
            var result = _context.Emprestimos
                .GroupBy(e => e.IdUsuario)
                .Select(g => new { UsuarioId = g.Key, TotalEmprestimos = g.Count() })
                .OrderByDescending(x => x.TotalEmprestimos)
                .ToList();

            return Ok(result);
        }

        // GET: api/relatorio/emprestimos-atrasados
        [HttpGet("emprestimos-atrasados")]
        public IActionResult EmprestimosAtrasados()
        {
            var result = _context.Emprestimos
                .Where(e => e.Status == Models.StatusEmprestimo.ATRASADO)
                .ToList();

            return Ok(result);
        }
    }
}
