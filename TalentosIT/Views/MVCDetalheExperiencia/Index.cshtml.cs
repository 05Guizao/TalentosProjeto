using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;

namespace TalentosIT.Views.MVCDetalheExperiencia
{
    public class IndexModel : PageModel
    {
        public string? MensagemSucesso { get; set; }
        public List<DetalheExperiencia> Experiencias { get; set; } = new();

        public void OnGet()
        {
            MensagemSucesso = TempData["Sucesso"] as string;
        }
    }
}