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

        public IEnumerable<Skill> ObterPorUtilizador(int? utilizadorId)
        {
            if (utilizadorId.HasValue)
            {
                return _context.Skills.Where(s => s.IdUtilizador == utilizadorId.Value).ToList();
            }
            else
            {
                return _context.Skills.Where(s => s.IdUtilizador == null).ToList(); // Skills globais
            }
        }

        public Skill ObterPorCodEUtilizador(int cod, int? utilizadorId)
        {
            return _context.Skills
                .Where(s => s.Cod == cod && (utilizadorId == null || s.IdUtilizador == utilizadorId))
                .FirstOrDefault();
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