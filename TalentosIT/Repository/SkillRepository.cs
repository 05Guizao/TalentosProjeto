using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;

namespace TalentosIT.Repository
{
    public class SkillRepository
    {
        private readonly ApplicationDbContext _context;
        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista só as skills do utilizador
        public IEnumerable<Skill> ObterPorUsuario(int userId)
        {
            return _context.Skills
                .Where(s => s.IdUtilizador == userId)
                .ToList();
        }

        // Obtem skill por id e userId (para editar/apagar)
        public Skill ObterPorIdEUsuario(int id, int userId)
        {
            return _context.Skills
                .FirstOrDefault(s => s.Cod == id && s.IdUtilizador == userId);
        }

        public void Adicionar(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }

        public void Atualizar(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
        }

        public void Remover(Skill skill)
        {
            _context.Skills.Remove(skill);
            _context.SaveChanges();
        }
    }
}