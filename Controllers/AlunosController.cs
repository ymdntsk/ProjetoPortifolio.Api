using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPortifolio.Api.Data;
using ProjetoPortifolio.Api.Dto;
using ProjetoPortifolio.Api.Models;

namespace ProjetoPortifolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerAlunoDto>>> GetAlunos()
        {
            var alunos = await _context.Alunos
                .Select(a => new VerAlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    NomeTurma = a.Turma != null ? a.Turma.Nome : null,
                    TotalProjetos = a.Projetos.Count
                })
                .ToListAsync();

            return Ok(alunos);
        }

        // Portfólio individual do aluno (com os projetos)
        [HttpGet("{id}/portfolio")]
        public async Task<ActionResult<AlunoPortifolioDto>> GetPortfolio(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .Include(a => a.Projetos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return NotFound("Aluno não encontrado");

            var portfolio = new AlunoPortifolioDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                NomeTurma = aluno.Turma?.Nome,
                Sobre = aluno.Sobre,
                AreaDeAtuacao = aluno.AreaDeAtuacao,
                Experiencia = aluno.Experiencia,
                Tecnologia = aluno.Tecnologia,
                HabilidadesCertificados = aluno.HabilidadesCertificados,
                Ferramentas = aluno.Ferramentas,
                PodePublicar = PortfolioCompleto(aluno),
                Projetos = aluno.Projetos.Select(p => new VerProjetoDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    TecnologiasUtilizadas = p.TecnologiasUtilizadas,
                    LinkDeAcesso = p.LinkDeAcesso,
                    ImagemVideo = p.ImagemVideo,
                    AlunoId = p.AlunoId
                }).ToList()
            };

            return Ok(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno(CriarAlunoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Informe o nome do aluno");

            if (dto.TurmaId != null && !await _context.Turmas.AnyAsync(t => t.id == dto.TurmaId))
                return BadRequest("Turma não encontrada");

            var aluno = new Aluno
            {
                Nome = dto.Nome,
                Sobre = dto.Sobre,
                AreaDeAtuacao = dto.AreaDeAtuacao,
                Experiencia = dto.Experiencia,
                Tecnologia = dto.Tecnologia,
                HabilidadesCertificados = dto.HabilidadesCertificados,
                Ferramentas = dto.Ferramentas,
                TurmaId = dto.TurmaId
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return Ok("Aluno criado com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, CriarAlunoDto dto)
        {
            var alunoExistente = await _context.Alunos.FindAsync(id);

            if (alunoExistente == null) return NotFound("Aluno não encontrado");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Informe o nome do aluno");

            if (dto.TurmaId != null && !await _context.Turmas.AnyAsync(t => t.id == dto.TurmaId))
                return BadRequest("Turma não encontrada");

            alunoExistente.Nome = dto.Nome;
            alunoExistente.Sobre = dto.Sobre;
            alunoExistente.AreaDeAtuacao = dto.AreaDeAtuacao;
            alunoExistente.Experiencia = dto.Experiencia;
            alunoExistente.Tecnologia = dto.Tecnologia;
            alunoExistente.HabilidadesCertificados = dto.HabilidadesCertificados;
            alunoExistente.Ferramentas = dto.Ferramentas;
            alunoExistente.TurmaId = dto.TurmaId;

            await _context.SaveChangesAsync();
            return Ok("Aluno editado com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Projetos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound("Aluno não encontrado");

            _context.Projetos.RemoveRange(aluno.Projetos);
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return Ok("Aluno removido com sucesso");
        }

        // Regra: 6 a 10 projetos e todas as seções do portfólio preenchidas
        private static bool PortfolioCompleto(Aluno aluno)
        {
            var totalProjetos = aluno.Projetos.Count;
            if (totalProjetos < 6 || totalProjetos > 10) return false;

            return !string.IsNullOrWhiteSpace(aluno.Sobre)
                && !string.IsNullOrWhiteSpace(aluno.AreaDeAtuacao)
                && !string.IsNullOrWhiteSpace(aluno.Experiencia)
                && !string.IsNullOrWhiteSpace(aluno.Tecnologia)
                && !string.IsNullOrWhiteSpace(aluno.HabilidadesCertificados)
                && !string.IsNullOrWhiteSpace(aluno.Ferramentas);
        }
    }
}
