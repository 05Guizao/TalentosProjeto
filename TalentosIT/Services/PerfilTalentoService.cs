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

        public async Task<PerfilTalento?> ObterOuCriarPerfilAsync(int userId)
        {
            return await _repository.ObterPerfilPorUtilizadorIdAsync(userId);
        }

        public async Task InserirPerfilAsync(PerfilTalento perfil)
        {
            await _repository.AdicionarAsync(perfil);
        }
        public async Task AtualizarPerfilAsync(PerfilTalento perfil)
        {
            _repository.Atualizar(perfil);
            await _repository.GravarAsync();
        }


    }
}