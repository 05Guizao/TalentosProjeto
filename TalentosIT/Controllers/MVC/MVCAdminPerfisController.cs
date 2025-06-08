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
    public async Task<IActionResult> EditarPerfil(PerfilTalento perfil)
    {
        if (ModelState.IsValid)
        {
            await _perfilService.AtualizarPerfilAsync(perfil);
            return RedirectToAction("Index");
        }

        TempData["Erro"] = "Erro ao editar perfil.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ApagarPerfil(int id)
    {
        await _perfilService.ApagarPerfilAsync(id);
        return RedirectToAction("Index");
    }
}

