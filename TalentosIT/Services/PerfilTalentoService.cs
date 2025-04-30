using TalentosIT.Models;
using TalentosIT.Repository;

namespace TalentosIT.Services
{
    public class PerfilTalentoService
    {
        private readonly PerfilTalentoRepository _repository;

        public PerfilTalentoService(PerfilTalentoRepository repository)
        {
            _repository = repository;
        }

        public PerfilTalento ObterOuCriarPerfil(int userId)
        {
            return _repository.ObterPerfilPorUtilizadorId(userId);
        }

        public void InserirPerfil(PerfilTalento perfil)
        {
            _repository.Adicionar(perfil);
        }
    }
}