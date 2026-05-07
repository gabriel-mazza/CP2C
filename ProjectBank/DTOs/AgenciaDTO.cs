namespace ProjectBank.DTOs
{
    public class AgenciaRequestDTO
    {
        public required string Numero { get; set; }
        public required string Nome { get; set; }
    }

    public class AgenciaResponseDTO
    {
        public int Id { get; set; }
        public required string Numero { get; set; }
        public required string Nome { get; set; }
    }
}
