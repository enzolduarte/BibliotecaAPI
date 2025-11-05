using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST: api/usuario
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Dados do usuário inválidos.");

            _usuarioService.CadastrarUsuario(usuario);
            return Ok("👤 Usuário cadastrado com sucesso!");
        }

        // GET: api/usuario
        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _usuarioService.ListarUsuarios();
            return Ok(usuarios);
        }

        // GET: api/usuario/{id}/pode-emprestar
        [HttpGet("{id}/pode-emprestar")]
        public IActionResult PodeEmprestar(int id)
        {
            bool pode = _usuarioService.PodeEmprestar(id);
            return Ok(new { usuarioId = id, podeEmprestar = pode });
        }
    }
}
