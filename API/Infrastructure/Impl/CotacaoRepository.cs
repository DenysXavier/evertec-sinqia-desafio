using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Impl
{
    public class CotacaoRepository : ICotacaoRepository
    {
        private CalculadoraDbContext _context;

        public CotacaoRepository(CalculadoraDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cotacao>> GetIntervaloAsync(DateTime dataAplicacao, DateTime dataFinal)
        {
            return await _context.Cotacoes.Where(cotacao => cotacao.Data >= dataAplicacao && cotacao.Data <= dataFinal).ToListAsync();
        }
    }
}
