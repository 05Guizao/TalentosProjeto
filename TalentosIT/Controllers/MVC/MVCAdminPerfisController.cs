using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC;

public class AdminPerfisController : Controller
{
    private readonly PerfilTalentoService _perfilService;

    public AdminPerfisController(PerfilTalentoService perfilService)
    {
        _perfilService = perfilService;
    }

    public async Task<IActionResult> Index()
    {
        var perfis = await _perfilService.ObterTodosAsync();
        return View(perfis);
    }

    [HttpPost]
    public async Task<IActionResult> EditarPerfil(int Cod, string Nome, string Email, string Pais, decimal PrecoHora)
    {
        var perfil = await _perfilService.ObterPorIdAsync(Cod);
        if (perfil == null)
        {
            TempData["Erro"] = "Perfil não encontrado.";
            return RedirectToAction("Index");
        }

        // Atualiza apenas os campos relevantes
        perfil.Nome = Nome;
        perfil.Email = Email;
        perfil.Pais = Pais;
        perfil.PrecoHora = PrecoHora;

        await _perfilService.AtualizarPerfilAsync(perfil);
        TempData["Sucesso"] = "Perfil atualizado com sucesso!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ApagarPerfil(int id)
    {
        await _perfilService.ApagarPerfilAsync(id);
        return RedirectToAction("Index");
    }
}

