using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TalentosIT.Models;
using TalentosIT.Services;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCDetalheExperienciaController : Controller
    {
        private readonly IDetalheExperienciaService _service;

        public MVCDetalheExperienciaController(IDetalheExperienciaService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var experiencias = _service.ObterPorUtilizador(userId.Value);
            return View(experiencias);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalheExperiencia experiencia)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                await _service.CriarAsync(userId.Value, experiencia);
                return RedirectToAction("Index");
            }

            return View(experiencia);
        }
    }
}