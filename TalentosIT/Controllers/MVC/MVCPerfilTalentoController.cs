using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC
{
    public class MVCPerfilTalentoController : Controller
    {
        private readonly SessaoUtilizadorService _sessaoUtilizador;
        private readonly PerfilTalentoService _perfilTalentoService;

        public MVCPerfilTalentoController(
            SessaoUtilizadorService sessaoUtilizador,
            PerfilTalentoService perfilTalentoService)
        {
            _sessaoUtilizador = sessaoUtilizador;
            _perfilTalentoService = perfilTalentoService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = await _perfilTalentoService.ObterOuCriarPerfilAsync(userId.Value);
            if (perfil == null)
                return RedirectToAction(nameof(Create));

            return View(perfil);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var utilizador = _sessaoUtilizador.ObterUtilizador();

            var model = new PerfilTalento
            {
                Nome  = utilizador.Nome,
                Email = utilizador.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerfilTalento perfil)
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            perfil.IdUtilizador = userId.Value;

            if (ModelState.IsValid)
            {
                await _perfilTalentoService.InserirPerfilAsync(perfil);
                return RedirectToAction(nameof(Index));
            }

            var utilizador = _sessaoUtilizador.ObterUtilizador();
            perfil.Nome  = utilizador.Nome;
            perfil.Email = utilizador.Email;

            return View(perfil);
        }
    }
}
