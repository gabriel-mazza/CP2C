namespace ProjectBank.Models
{
    public class PessoaFisica : Cliente
    {
        public required string Cpf { get; set; } 
        public DateTime DataNascimento { get; set; } 
    }
}
