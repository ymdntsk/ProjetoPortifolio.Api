using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using ProjetoPortifolio.Api.Models;
using ProjetoPortifolio.Api.Data;

namespace ProjetoPortifolio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario user)
        {
            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.User == user.User && u.Senha == user.Senha);

            if (usuarioEncontrado == null)
                return Unauthorized("Credenciais inválidas");

            
            return Ok(new { User = usuarioEncontrado.User });
        }
    }
} 