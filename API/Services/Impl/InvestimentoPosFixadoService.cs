using API.Helper;
using API.Models;
using DecimalMath;

namespace API.Services.Impl
{
    public class InvestimentoPosFixadoService : IInvestimentoPosFixadoService
    {
        public AplicacaoCalculada CalcularBaseadoNosIndices(decimal valorInvestido, IEnumerable<Cotacao> cotacoes)
        {
            decimal fatorAcumulado = 1;

            foreach (var cotacao in cotacoes)
            {
                fatorAcumulado *= this.FatorDiario(cotacao.Valor);
            }
            fatorAcumulado = OperacaoDecimal.Truncar(fatorAcumulado, 16);

            var aplicacaoCalculada = new AplicacaoCalculada
            {
                ValorAtualizadoDoInvestimento = OperacaoDecimal.Truncar(valorInvestido * fatorAcumulado, 8),
                FatorAcumuladoDeJuros = fatorAcumulado
            };

            return aplicacaoCalculada;
        }

        public decimal FatorDiario(decimal taxaAnual)
        {
            var fracaoDeDiasUteisAoAno = 1m / 252;
            var taxaAnualPorcento = (taxaAnual / 100) + 1;
            var fatorDiario = DecimalEx.Pow(taxaAnualPorcento, fracaoDeDiasUteisAoAno);
            return Math.Round(fatorDiario, 8);
        }

    }
}
