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

        public IEnumerable<Skill> ObterSkillsDoUsuario(int userId)
            => _repo.ObterPorUsuario(userId);

        public Skill ObterSkill(int id, int userId)
            => _repo.ObterPorIdEUsuario(id, userId);

        public void CriarSkill(Skill skill)
            => _repo.Adicionar(skill);

        public void AtualizarSkill(Skill skill)
            => _repo.Atualizar(skill);

        public void RemoverSkill(int id, int userId)
        {
            var skill = _repo.ObterPorIdEUsuario(id, userId);
            if (skill != null)
                _repo.Remover(skill);
        }
    }
}