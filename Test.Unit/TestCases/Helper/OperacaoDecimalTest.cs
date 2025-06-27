using API.Helper;

namespace Test.Unit.TestCases.Helper
{
    public class OperacaoDecimalTest
    {

        [Theory]
        [InlineData(1.1238, 1.123)]
        [InlineData(2.9999, 2.999)]
        public void DadoUmDecimal_AoTruncalo_OValorResultanteNaoPodeSerArredondado(decimal valorASerTruncado, decimal valorEsperado)
        {
            var valorCalculado = OperacaoDecimal.Truncar(valorASerTruncado, 3);

            Assert.Equal(valorEsperado, valorCalculado);
        }
    }
}
