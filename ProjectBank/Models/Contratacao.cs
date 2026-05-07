namespace ProjectBank.Models
{
    public enum StatusContratacao
    {
        Pendente = 0,
        Aprovada = 1,
        Recusada = 2
    }

    public class Contratacao
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
        public StatusContratacao Status { get; set; } = StatusContratacao.Pendente;
        public string? Observacao { get; set; }
        public decimal? TaxaMdrEfetiva { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
    }
}
