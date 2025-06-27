using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class CalculadoraDbContext : DbContext
    {
        public CalculadoraDbContext(DbContextOptions<CalculadoraDbContext> options) : base(options)
        {

        }

        public DbSet<Cotacao> Cotacoes { get; set; }
    }
}
