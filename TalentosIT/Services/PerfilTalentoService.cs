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
            var perfil = _repository.ObterPerfilPorUtilizadorId(userId);
            return perfil;
        }
    }
}