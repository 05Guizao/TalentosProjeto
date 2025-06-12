using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.EntityFrameworkCore;

namespace TalentosIT.Repository
{
    public class PropostaTrabalhoRepository
    {
        private readonly ApplicationDbContext _context;

        public PropostaTrabalhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PropostaTrabalho>> ObterTodasAsync()
        {
            return await _context.PropostaTrabalhos.AsNoTracking().ToListAsync();
        }

        public async Task<PropostaTrabalho?> ObterPorIdAsync(int id)
        {
            return await _context.PropostaTrabalhos.FindAsync(id);
        }

        public void Atualizar(PropostaTrabalho proposta)
        {
            _context.PropostaTrabalhos.Update(proposta);
        }

        public async Task ApagarAsync(int id)
        {
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);
            if (proposta != null)
            {
                _context.PropostaTrabalhos.Remove(proposta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task GravarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}