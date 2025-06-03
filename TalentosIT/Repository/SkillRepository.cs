using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Skill> ObterTodas()
        {
            return _context.Skills.ToList(); // Todas são globais agora
        }

        public Skill ObterPorCod(int cod)
        {
            return _context.Skills.FirstOrDefault(s => s.Cod == cod);
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

        public void Remover(int cod)
        {
            var skill = ObterPorCod(cod);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Skill> ObterGlobais()
        {
            return _context.Skills.Where(s => s.Estado == "Ativo").ToList();
        }

    }
}