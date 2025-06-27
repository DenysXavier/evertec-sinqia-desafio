using API.Models;
using API.Services.Impl;

namespace Test.Unit.TestCases.Services.Impl
{
    public class InvestimentoPosFixadoServiceTest
    {
        private InvestimentoPosFixadoService _investimentoServiceSUT;

        public InvestimentoPosFixadoServiceTest()
        {
            _investimentoServiceSUT = new InvestimentoPosFixadoService();
        }

        [Theory]
        [InlineData(12, 1.00044982)]
        [InlineData(24, 1.00085398)]
        [InlineData(48, 1.00155693)]
        public void DadaUmaTaxaAnualValida_AoCalcular_OFatorDiarioDessaTaxaEhRetornado(decimal taxaAnual, decimal fatorDiarioEsperado)
        {
            // Act
            var fatorDiarioObtido = _investimentoServiceSUT.FatorDiario(taxaAnual);

            // Assert
            Assert.Equal(fatorDiarioEsperado, fatorDiarioObtido);
        }

        [Fact]
        public void DadoUmValorInvestido_AoCalcularBaseadoNasCotacoesDiarias_AAplicacaoCalculadaNoFimDoPeriodoEhRetornada()
        {
            // Arrange
            var valorInvestido = 10000;
            var cotacoes = new List<Cotacao>(){
                new() { Valor = 12 },
                new() { Valor = 12.5m },
                new() { Valor = 11 }
            };
            var valorEsperado = 10013.32m;
            var jurosAcumuladoEsperado = 1.00133212034107m;

            // Act
            var resultado = _investimentoServiceSUT.CalcularBaseadoNosIndices(valorInvestido, cotacoes);

            // Assert
            Assert.Equal(valorEsperado, resultado.ValorAtualizadoDoInvestimento, 2);
            Assert.Equal(jurosAcumuladoEsperado, resultado.FatorAcumuladoDeJuros, 14);
        }
    }
}
