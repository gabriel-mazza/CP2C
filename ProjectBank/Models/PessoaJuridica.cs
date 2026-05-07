namespace ProjectBank.Models
{
    public class PessoaJuridica : Cliente
    {
        public required string Cnpj { get; set; } 
        public required string RazaoSocial { get; set; }  
    }
}
