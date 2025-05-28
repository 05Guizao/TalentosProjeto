using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            Console.WriteLine($"🟡 A procurar perfil com idUtilizador = {userId}");

            return _context.PerfilTalentos
                .AsNoTracking()
                .FirstOrDefault(p => p.IdUtilizador == userId);
        }

        public void Adicionar(PerfilTalento perfil)
        {
            _context.PerfilTalentos.Add(perfil);
            _context.SaveChanges();
        }
    }
}