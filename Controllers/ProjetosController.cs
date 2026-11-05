using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPortifolio.Api.Data;
using ProjetoPortifolio.Api.Dto;
using ProjetoPortifolio.Api.Models;

namespace ProjetoPortifolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerProjetoDto>>> GetProjetos()
        {
            var projetos = await _context.Projetos
                .Select(p => new VerProjetoDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    TecnologiasUtilizadas = p.TecnologiasUtilizadas,
                    LinkDeAcesso = p.LinkDeAcesso,
                    ImagemVideo = p.ImagemVideo,
                    AlunoId = p.AlunoId
                })
                .ToListAsync();

            return Ok(projetos);
        }

        [HttpPost]
        public async Task<IActionResult> CriarProjeto(CriarProjetoDto dto)
        {
            var projeto = new Projeto
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                TecnologiasUtilizadas = dto.TecnologiasUtilizadas,
                LinkDeAcesso = dto.LinkDeAcesso,
                ImagemVideo = dto.ImagemVideo,
                AlunoId = dto.AlunoId
            };

            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return Ok("Projeto criado com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjeto(int id, CriarProjetoDto dto)
        {
            var projetoExistente = await _context.Projetos.FindAsync(id);

            if (projetoExistente == null) return NotFound("Projeto não encontrado");

            projetoExistente.Titulo = dto.Titulo;
            projetoExistente.Descricao = dto.Descricao;
            projetoExistente.TecnologiasUtilizadas = dto.TecnologiasUtilizadas;
            projetoExistente.LinkDeAcesso = dto.LinkDeAcesso;
            projetoExistente.ImagemVideo = dto.ImagemVideo;

            await _context.SaveChangesAsync();
            return Ok("Projeto editado com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
                return NotFound("Projeto não encontrado");

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();
            return Ok("Projeto removido com sucesso");
        }
    }
}
