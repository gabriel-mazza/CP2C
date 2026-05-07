namespace ProjectBank.Models
{
    public abstract class Cliente
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public int AgenciaId { get; set; }
        public Agencia Agencia { get; set; } = null!;

       
        public ICollection<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();
    }
}
