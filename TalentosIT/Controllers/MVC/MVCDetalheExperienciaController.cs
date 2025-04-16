using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCDetalheExperienciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCDetalheExperienciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null)
                return RedirectToAction("Create", "MVCPerfilTalento");

            var experiencias = _context.DetalheExperiencias
                .Where(e => e.CodPerfilTalento == perfil.Cod)
                .ToList();

            return View(experiencias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalheExperiencia experiencia)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null)
                return RedirectToAction("Create", "MVCPerfilTalento");

            experiencia.CodPerfilTalento = perfil.Cod;

            if (ModelState.IsValid)
            {
                _context.DetalheExperiencias.Add(experiencia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(experiencia);
        }
    }
}
