using System.Collections.Generic;
using TalentosIT.Models;
using TalentosIT.Repository;

namespace TalentosIT.Services
{
    public class SkillService
    {
        private readonly SkillRepository _repo;

        public SkillService(SkillRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Skill> ObterTodasSkills()
        {
            return _repo.ObterTodas();
        }

        public IEnumerable<Skill> ObterTodasSkillsGlobais()
        {
            return _repo.ObterGlobais();
        }

        public Skill ObterSkill(int cod)
        {
            return _repo.ObterPorCod(cod);
        }

        public void CriarSkill(Skill skill)
        {
            _repo.Adicionar(skill);
        }

        public void AtualizarSkill(Skill skill)
        {
            _repo.Atualizar(skill);
        }

        public void RemoverSkill(int cod)
        {
            _repo.Remover(cod);
        }
    }
}