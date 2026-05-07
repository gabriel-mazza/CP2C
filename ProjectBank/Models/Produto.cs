namespace ProjectBank.Models
{
    public abstract class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Ativo { get; set; } = 1; // Oracle não aceita bool — usa 1/0

        public ICollection<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();
    }

    public class MaquinaDeCartao : Produto
    {
        public string ModeloEquipamento { get; set; } = string.Empty;
        public decimal TaxaMdrBase { get; set; }
    }
}
