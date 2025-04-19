using TalentosIT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TalentosIT.Services
{
    public interface IDetalheExperienciaService
    {
        List<DetalheExperiencia> ObterPorUtilizador(int userId);
        Task CriarAsync(int userId, DetalheExperiencia experiencia);
    }
}