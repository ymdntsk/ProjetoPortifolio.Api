using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPortifolio.Api.Data;
using ProjetoPortifolio.Api.Dto;
using ProjetoPortifolio.Api.Models;

namespace ProjetoPortifolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerTurmaDto>>> GetTurmas()
        {
            var turmas = await _context.Turmas
                .Select(t => new VerTurmaDto
                {
                    Id = t.id,
                    Nome = t.Nome,
                    TotalAlunos = t.Alunos.Count
                })
                .ToListAsync();

            return Ok(turmas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarTurma(CriarTurmaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Informe o nome da turma");

            var turma = new Turma
            {
                Nome = dto.Nome
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return Ok("Turma criada com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurma(int id, CriarTurmaDto dto)
        {
            var turmaExistente = await _context.Turmas.FindAsync(id);

            if (turmaExistente == null) return NotFound("Turma não encontrada");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Informe o nome da turma");

            turmaExistente.Nome = dto.Nome;

            await _context.SaveChangesAsync();
            return Ok("Turma editada com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada");


            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return Ok("Turma removida com sucesso");
        }
    }
}
