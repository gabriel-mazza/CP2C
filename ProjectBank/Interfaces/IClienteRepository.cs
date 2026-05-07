using ProjectBank.Models;

namespace ProjectBank.Interfaces
{
    public interface IClienteRepository
    {
        Task<PessoaFisica> AdicionarPFAsync(PessoaFisica pessoaFisica);
        Task<PessoaJuridica> AdicionarPJAsync(PessoaJuridica pessoaJuridica);
        Task<Cliente?> ObterPorIdAsync(int id);

        Task<bool> ExisteCpfAsync(string cpf);
        Task<bool> ExisteCnpjAsync(string cnpj);
    }
}
