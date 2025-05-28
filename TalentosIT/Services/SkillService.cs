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

        public IEnumerable<Skill> ObterSkillsDoUtilizador(int? utilizadorId)
        {
            if (utilizadorId.HasValue)
                return _repo.ObterPorUtilizador(utilizadorId.Value);

            return _repo.ObterPorUtilizador(null); // Skills globais
        }

        public Skill ObterSkill(int cod, int? utilizadorId)
        {
            return _repo.ObterPorCodEUtilizador(cod, utilizadorId);
        }

        public void CriarSkill(Skill skill)
        {
            _repo.Adicionar(skill);
        }

        public void AtualizarSkill(Skill skill)
        {
            _repo.Atualizar(skill);
        }

        public void RemoverSkill(int cod, int? utilizadorId)
        {
            var skill = _repo.ObterPorCodEUtilizador(cod, utilizadorId);
            if (skill != null)
                _repo.Remover(skill);
        }
    }
}