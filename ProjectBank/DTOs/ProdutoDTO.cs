namespace ProjectBank.DTOs
{
    public class MaquinaDeCartaoRequestDTO
    {
        public required string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string ModeloEquipamento { get; set; } = string.Empty;
        public decimal TaxaMdrBase { get; set; }
    }

    public class ProdutoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ModeloEquipamento { get; set; } = string.Empty;
        public decimal TaxaMdrBase { get; set; }
    }
}
