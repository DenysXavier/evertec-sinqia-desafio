using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("cotacao")]
    public class Cotacao
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public string Indexador { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
