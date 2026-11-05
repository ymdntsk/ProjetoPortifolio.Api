using Microsoft.EntityFrameworkCore;

namespace ProjetoPortifolio.Api.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions options) : base(options)
            {
            }

        }
    }
