using Microsoft.EntityFrameworkCore;
using ProjetoPortifolio.Api.Models;

namespace ProjetoPortifolio.Api.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions options) : base(options)
            {
            }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
    }
