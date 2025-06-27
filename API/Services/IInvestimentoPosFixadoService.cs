using API.Models;

namespace API.Services
{
    public interface IInvestimentoPosFixadoService
    {
        AplicacaoCalculada CalcularBaseadoNosIndices(decimal valorInvestido, IEnumerable<Cotacao> cotacoes);
    }
}
