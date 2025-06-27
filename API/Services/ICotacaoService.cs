using API.Models;

namespace API.Services
{
    public interface ICotacaoService
    {
        Task<List<Cotacao>> GetIntervaloAsync(DateOnly dataAplicacao, DateOnly dataFinal);
    }
}
