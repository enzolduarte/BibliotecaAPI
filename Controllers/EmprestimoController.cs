using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;

        public EmprestimoController(EmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        // POST: api/emprestimo
        [HttpPost]
        public IActionResult Registrar([FromBody] EmprestimoRequest request)
        {
            try
            {
                _emprestimoService.RegistrarEmprestimo(request.UsuarioId, request.ISBNLivro);
                return Ok("📗 Empréstimo registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        // PUT: api/emprestimo/{id}/devolver
        [HttpPut("{id}/devolver")]
        public IActionResult Devolver(int id)
        {
            try
            {
                _emprestimoService.RegistrarDevolucao(id);
                return Ok("📕 Devolução registrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        // GET: api/emprestimo
        [HttpGet]
        public IActionResult Listar()
        {
            var emprestimos = _emprestimoService.ListarEmprestimos();
            return Ok(emprestimos);
        }
    }

    // DTO (objeto usado para enviar dados no POST)
    public class EmprestimoRequest
    {
        public int UsuarioId { get; set; }
        public string ISBNLivro { get; set; }
    }
}
