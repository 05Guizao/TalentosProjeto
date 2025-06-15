using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TalentosIT.Models;
using TalentosIT.Services;
using System.Threading.Tasks;
using TalentosIT.Views.MVCDetalheExperiencia;
using System.Linq;
using TalentosIT.Data;

namespace TalentosIT.Controllers
{
    public class MVCDetalheExperienciaController : Controller
    {
        private readonly IDetalheExperienciaService _service;
        private readonly ApplicationDbContext _context;

        public MVCDetalheExperienciaController(IDetalheExperienciaService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var experiencias = _service.ObterPorUtilizador(userId.Value);
            var model = new IndexModel
            {
                Experiencias = experiencias,
                MensagemSucesso = TempData["Sucesso"] as string
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Verifica se o utilizador tem um perfil associado
            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId.Value);
            if (perfil == null)
            {
                TempData["Erro"] = "Precisa de criar o seu perfil antes de adicionar experiências.";
                return RedirectToAction("Create", "MVCPerfilTalento");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalheExperiencia experiencia)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                await _service.CriarAsync(userId.Value, experiencia);
                TempData["Sucesso"] = "Experiência adicionada com sucesso!";
                return RedirectToAction("Index");
            }

            return View(experiencia);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var experiencia = await _service.ObterPorIdAsync(id);
            if (experiencia == null) return NotFound();
            return View(experiencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DetalheExperiencia experiencia)
        {
            ModelState.Remove(nameof(DetalheExperiencia.PerfilTalento)); 

            if (ModelState.IsValid)
            {
                await _service.AtualizarAsync(experiencia);
                TempData["Sucesso"] = "Experiência atualizada com sucesso!";
                return RedirectToAction("Index");
            }

            return View(experiencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarAsync(id);
            TempData["Sucesso"] = "Experiência eliminada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
