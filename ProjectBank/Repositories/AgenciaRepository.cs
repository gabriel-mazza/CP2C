using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Interfaces;
using ProjectBank.Models;

namespace ProjectBank.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly BancoDbContext _context;

        public AgenciaRepository(BancoDbContext context)
        {
            _context = context;
        }

        public async Task<Agencia> AdicionarAsync(Agencia agencia)
        {
            await _context.Agencias.AddAsync(agencia);
            await _context.SaveChangesAsync();
            return agencia;
        }

        public async Task<Agencia?> ObterPorIdAsync(int id)
        {
            return await _context.Agencias.FirstOrDefaultAsync(a => a.Id == id);
        }

        // AnyAsync não funciona no Oracle — usa CountAsync > 0
        public async Task<bool> ExisteAgenciaAsync(int id)
        {
            return await _context.Agencias.CountAsync(a => a.Id == id) > 0;
        }
    }
}
