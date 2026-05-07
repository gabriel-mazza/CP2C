namespace ProjectBank.Models
{
    public class Agencia
    {
        public int Id { get; set; }
        public required string Numero { get; set; }
        public required string Nome { get; set; }

        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
