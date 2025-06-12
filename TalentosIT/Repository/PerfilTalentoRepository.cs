using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace TalentosIT.Repository
{
    public class PerfilTalentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilTalentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PerfilTalento?> ObterPerfilPorUtilizadorIdAsync(int userId)
        {
            Console.WriteLine($"🔍 A procurar perfil com IdUtilizador = {userId}...");

            var sw = Stopwatch.StartNew();
            var perfil = await _context.PerfilTalentos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            sw.Stop();

            Console.WriteLine($"✅ Query executada em {sw.ElapsedMilliseconds} ms");

            return perfil;
        }

        public async Task AdicionarAsync(PerfilTalento perfil)
        {
            await _context.PerfilTalentos.AddAsync(perfil);
            await _context.SaveChangesAsync();
        }

        public void Atualizar(PerfilTalento perfil)
        {
            _context.PerfilTalentos.Update(perfil);
        }

        public async Task GravarAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<PerfilTalento>> ObterTodosAsync()
        {
            return await _context.PerfilTalentos.ToListAsync();
        }

        public async Task ApagarAsync(int id)
        {
            var perfil = await _context.PerfilTalentos.FindAsync(id);
            if (perfil != null)
            {
                _context.PerfilTalentos.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PerfilTalento?> ObterPorIdAsync(int id)
        {
            return await _context.PerfilTalentos.FindAsync(id);
        }
        
        public async Task<List<PerfilTalento>> ObterTodosComPropostasAsync()
        {
            return await _context.PerfilTalentos
                .Include(p => p.PropostasTrabalho)
                .ToListAsync();
        }
    }
}
