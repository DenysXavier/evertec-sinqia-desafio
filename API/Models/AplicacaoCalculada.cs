using System.Drawing;

namespace API.Models
{
    public class AplicacaoCalculada
    {
        public decimal FatorAcumuladoDeJuros { get; set; } = 1;
        public decimal ValorAtualizadoDoInvestimento { get; set; } = 0;
    }
}
