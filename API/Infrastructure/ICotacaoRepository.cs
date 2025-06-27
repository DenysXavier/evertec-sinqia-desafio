using API.Models;

namespace API.Infrastructure
{
    public interface ICotacaoRepository
    {
        Task<List<Cotacao>> GetIntervaloAsync(DateTime dataAplicacao, DateTime dataFinal);
    }
}
