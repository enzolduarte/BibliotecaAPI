using BibliotecaAPI.Data;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public class UsuarioService
    {
        private readonly BibliotecaContext _context;

        public UsuarioService(BibliotecaContext context)
        {
            _context = context;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public bool PodeEmprestar(int userId)
        {
            int ativos = _context.Emprestimos.Count(e => e.IdUsuario == userId && e.Status == StatusEmprestimo.ATIVO);
            return ativos < 3;
        }

        public List<Usuario> ListarUsuarios()
        {
            return _context.Usuarios.ToList();
        }

    }
}
