namespace API.Helper
{
    public static class OperacaoDecimal
    {
        public static decimal Truncar(decimal valor, int truncarEm)
        {
            var divisor = (decimal)Math.Pow(10, -1 * truncarEm);
            var valorTruncado = (valor - (valor % divisor));
            return valorTruncado;
        }
    }
}
