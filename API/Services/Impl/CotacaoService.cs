using API.Infrastructure;
using API.Models;

namespace API.Services.Impl
{
    public class CotacaoService : ICotacaoService
    {
        private ICotacaoRepository _repo;

        public CotacaoService(ICotacaoRepository repository)
        {
            _repo = repository;
        }

        public async Task<List<Cotacao>> GetIntervaloAsync(DateOnly dataAplicacao, DateOnly dataFinal)
        {
            return await _repo.GetIntervaloAsync(
                dataAplicacao.ToDateTime(TimeOnly.MinValue),
                dataFinal.ToDateTime(TimeOnly.MinValue));
        }
    }
}
