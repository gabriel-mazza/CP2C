namespace ProjectBank.DTOs
{
    public class PessoaFisicaRequestDTO
    {
        public required string Nome { get; set; }
        public int AgenciaId { get; set; }
        public required string Cpf { get; set; } 
        public DateTime DataNascimento { get; set; }
    }

    public class PessoaJuridicaRequestDTO
    {
        public required string Nome { get; set; }
        public int AgenciaId { get; set; }
        public required string Cnpj { get; set; } 
        public required string RazaoSocial { get; set; }
    }

    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int AgenciaId { get; set; }
        public required string TipoCliente { get; set; } 

        public string? Documento { get; set; } 
    }
}
