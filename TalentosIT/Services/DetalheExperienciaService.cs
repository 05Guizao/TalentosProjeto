using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentosIT.Data;
using TalentosIT.Models;

namespace TalentosIT.Services
{
    public class DetalheExperienciaService : IDetalheExperienciaService
    {
        private readonly ApplicationDbContext _context;

        public DetalheExperienciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DetalheExperiencia> ObterPorUtilizador(int userId)
        {
            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null) return new List<DetalheExperiencia>();

            return _context.DetalheExperiencias
                .Where(e => e.CodPerfilTalento == perfil.Cod)
                .ToList();
        }

        public async Task<DetalheExperiencia> ObterPorIdAsync(int id)
        {
            return await _context.DetalheExperiencias.FindAsync(id);
        }

        public async Task CriarAsync(int userId, DetalheExperiencia experiencia)
        {
            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null) return;

            experiencia.CodPerfilTalento = perfil.Cod;
            _context.DetalheExperiencias.Add(experiencia);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(DetalheExperiencia experiencia)
        {
            _context.DetalheExperiencias.Update(experiencia);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var experiencia = await _context.DetalheExperiencias.FindAsync(id);
            if (experiencia != null)
            {
                _context.DetalheExperiencias.Remove(experiencia);
                await _context.SaveChangesAsync();
            }
        }
    }
}