using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Interfaces;
using ProjectBank.Models;

namespace ProjectBank.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BancoDbContext _context;

        public ClienteRepository(BancoDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaFisica> AdicionarPFAsync(PessoaFisica pessoaFisica)
        {
            await _context.PessoasFisicas.AddAsync(pessoaFisica);
            await _context.SaveChangesAsync();
            return pessoaFisica;
        }

        public async Task<PessoaJuridica> AdicionarPJAsync(PessoaJuridica pessoaJuridica)
        {
            await _context.PessoasJuridicas.AddAsync(pessoaJuridica);
            await _context.SaveChangesAsync();
            return pessoaJuridica;
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes.Include(c => c.Agencia).FirstOrDefaultAsync(c => c.Id == id);
        }

        // AnyAsync não funciona no Oracle — usa CountAsync > 0
        public async Task<bool> ExisteCpfAsync(string cpf)
        {
            return await _context.PessoasFisicas.CountAsync(pf => pf.Cpf == cpf) > 0;
        }

        public async Task<bool> ExisteCnpjAsync(string cnpj)
        {
            return await _context.PessoasJuridicas.CountAsync(pj => pj.Cnpj == cnpj) > 0;
        }
    }
}
