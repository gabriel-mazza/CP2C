using ProjectBank.Models;

namespace ProjectBank.Interfaces
{
    public interface IAgenciaRepository
    {
        Task<Agencia> AdicionarAsync(Agencia agencia);
        Task<Agencia?> ObterPorIdAsync(int id);
        Task<bool> ExisteAgenciaAsync(int id); 
    }
}
