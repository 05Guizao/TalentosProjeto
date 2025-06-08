using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC;

public class AdminPropostasController : Controller
{
    private readonly PropostaTrabalhoService _propostaService;

    public AdminPropostasController(PropostaTrabalhoService propostaService)
    {
        _propostaService = propostaService;
    }

    public async Task<IActionResult> Index()
    {
        var propostas = await _propostaService.ObterTodasAsync();
        return View(propostas);
    }

    [HttpPost]
    public async Task<IActionResult> EditarProposta(PropostaTrabalho proposta)
    {
        if (ModelState.IsValid)
        {
            await _propostaService.AtualizarAsync(proposta);
            return RedirectToAction("Index");
        }

        TempData["Erro"] = "Erro ao editar proposta.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ApagarProposta(int id)
    {
        await _propostaService.ApagarAsync(id);
        return RedirectToAction("Index");
    }
}