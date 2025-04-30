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

        public IActionResult Index()
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _perfilTalentoService.ObterOuCriarPerfil(userId.Value);
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

            // Pré-preenche Nome e Email:
            var utilizador = _sessaoUtilizador.ObterUtilizador(); 
            // -> implementa em SessaoUtilizadorService: ObterUtilizador() que devolve o objeto Utilizador

            var model = new PerfilTalento
            {
                Nome  = utilizador.Nome,
                Email = utilizador.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PerfilTalento perfil)
        {
            var userId = _sessaoUtilizador.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            perfil.IdUtilizador = userId.Value;

            if (ModelState.IsValid)
            {
                _perfilTalentoService.InserirPerfil(perfil);
                return RedirectToAction(nameof(Index));
            }

            // Se falhar validação, mantém Nome/Email preenchidos
            var utilizador = _sessaoUtilizador.ObterUtilizador();
            perfil.Nome  = utilizador.Nome;
            perfil.Email = utilizador.Email;

            return View(perfil);
        }
    }
}
