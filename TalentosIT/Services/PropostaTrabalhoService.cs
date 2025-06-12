using TalentosIT.Models;
using TalentosIT.Repository;

namespace TalentosIT.Services
{
    public class PropostaTrabalhoService
    {
        private readonly PropostaTrabalhoRepository _repository;

        public PropostaTrabalhoService(PropostaTrabalhoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PropostaTrabalho>> ObterTodasAsync()
        {
            return await _repository.ObterTodasAsync();
        }

        public async Task<PropostaTrabalho?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AtualizarAsync(PropostaTrabalho proposta)
        {
            _repository.Atualizar(proposta);
            await _repository.GravarAsync();
        }

        public async Task ApagarAsync(int id)
        {
            await _repository.ApagarAsync(id);
        }
    }
}