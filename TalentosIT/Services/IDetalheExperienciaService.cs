using System.Collections.Generic;
using System.Threading.Tasks;
using TalentosIT.Models;

namespace TalentosIT.Services
{
    public interface IDetalheExperienciaService
    {
        List<DetalheExperiencia> ObterPorUtilizador(int userId);
        Task<DetalheExperiencia> ObterPorIdAsync(int id);
        Task CriarAsync(int userId, DetalheExperiencia experiencia);
        Task AtualizarAsync(DetalheExperiencia experiencia);
        Task EliminarAsync(int id); // <- este é o nome correto para corresponder ao controller e à implementação
    }
}