using TalentosIT.Models;
using TalentosIT.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<PerfilTalento>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task ApagarPerfilAsync(int id)
        {
            await _repository.ApagarAsync(id);
        }

        public async Task<PerfilTalento?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }
        
        public async Task<List<PerfilTalento>> ObterTodosComPropostasAsync()
        {
            return await _repository.ObterTodosComPropostasAsync();
        }

    }
}