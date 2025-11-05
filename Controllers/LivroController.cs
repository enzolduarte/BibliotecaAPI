using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivroController(LivroService livroService)
        {
            _livroService = livroService;
        }

        // POST: api/livro
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Livro livro)
        {
            if (livro == null)
                return BadRequest("Dados do livro inválidos.");

            _livroService.CadastrarLivro(livro);
            return Ok("📘 Livro cadastrado com sucesso!");
        }

        // GET: api/livro
        [HttpGet]
        public IActionResult Listar()
        {
            var livros = _livroService.ListarLivros();
            return Ok(livros);
        }

        // PUT: api/livro/{isbn}/status
        [HttpPut("{isbn}/status")]
        public IActionResult AtualizarStatus(string isbn, [FromQuery] StatusLivro status)
        {
            _livroService.AtualizarStatus(isbn, status);
            return Ok("✅ Status do livro atualizado!");
        }
    }
}
