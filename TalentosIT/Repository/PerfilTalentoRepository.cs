using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;

namespace TalentosIT.Repository
{
    public class PerfilTalentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilTalentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PerfilTalento ObterPerfilPorUtilizadorId(int userId)
        {
            return _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
        }
    }
}