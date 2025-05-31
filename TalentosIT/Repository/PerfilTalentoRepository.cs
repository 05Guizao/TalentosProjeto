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


    }
}