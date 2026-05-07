namespace ProjectBank.DTOs
{
    public class ContratacaoRequestDTO
    {
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
    }

    public class ContratacaoResponseDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal? TaxaMdrEfetiva { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}
