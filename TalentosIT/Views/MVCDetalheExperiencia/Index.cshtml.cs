using TalentosIT.Models;
using System.Collections.Generic;

namespace TalentosIT.Views.MVCDetalheExperiencia
{
    public class IndexModel
    {
        public string? MensagemSucesso { get; set; }
        public List<DetalheExperiencia> Experiencias { get; set; } = new();
    }
}