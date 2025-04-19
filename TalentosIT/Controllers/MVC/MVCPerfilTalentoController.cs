using Microsoft.AspNetCore.Mvc;
using TalentosIT.Services;
using TalentosIT.Models;

namespace TalentosIT.Controllers.MVC
{
    public class MVCPerfilTalentoController : Controller
    {
        private readonly SessaoUtilizadorService _sessaoUtilizador;
        private readonly PerfilTalentoService _perfilTalentoService;

        public MVCPerfilTalentoController(SessaoUtilizadorService sessaoUtilizador, PerfilTalentoService perfilTalentoService)
        {
            _sessaoUtilizador = sessaoUtilizador;
            _perfilTalentoService = perfilTalentoService;
        }

        public IActionResult Index()
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _perfilTalentoService.ObterOuCriarPerfil(userId.Value);

            if (perfil == null)
                return RedirectToAction("Create");

            return View(perfil);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PerfilTalento perfil)
        {
            if (ModelState.IsValid)
            {
                // Aqui podias eventualmente usar o serviço para guardar também
                return RedirectToAction(nameof(Index));
            }

            return View(perfil);
        }
    }
}