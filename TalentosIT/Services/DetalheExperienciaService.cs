using System;
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
            if (perfil == null)
            {
                Console.WriteLine($"[ERRO] Perfil não encontrado para utilizador com ID {userId}");
                return new List<DetalheExperiencia>();
            }

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
            if (perfil == null)
                throw new Exception($"Perfil não encontrado para o utilizador {userId}");

            // Associar a experiência ao perfil do cliente
            experiencia.CodPerfilTalento = perfil.Cod;

            _context.DetalheExperiencias.Add(experiencia);
            await _context.SaveChangesAsync();

            Console.WriteLine($"[INFO] Experiência criada para perfil ID {perfil.Cod}");
        }

        public async Task AtualizarAsync(DetalheExperiencia experiencia)
        {
            _context.DetalheExperiencias.Update(experiencia);
            await _context.SaveChangesAsync();

            Console.WriteLine($"[INFO] Experiência com ID {experiencia.Cod} atualizada.");
        }

        public async Task EliminarAsync(int id)
        {
            var experiencia = await _context.DetalheExperiencias.FindAsync(id);
            if (experiencia != null)
            {
                _context.DetalheExperiencias.Remove(experiencia);
                await _context.SaveChangesAsync();

                Console.WriteLine($"[INFO] Experiência com ID {id} eliminada.");
            }
            else
            {
                Console.WriteLine($"[ERRO] Experiência com ID {id} não encontrada para eliminar.");
            }
        }
    }
}
