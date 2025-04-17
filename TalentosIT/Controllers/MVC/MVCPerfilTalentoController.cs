using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using System.Threading.Tasks;
using TalentosIT.Views.BemVindoPerfil;

namespace TalentosIT.Controllers
{
    public class MVCPerfilTalentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCPerfilTalentoController(ApplicationDbContext context)
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
                return RedirectToAction("Create");

            return View(perfil);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfilExistente = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfilExistente != null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerfilTalento perfil)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Content("UserId NULL — sessão não iniciada!");

            var existingPerfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (existingPerfil != null)
                return Content("Perfil já existe — redirecionar para Index!");

            perfil.IdUtilizador = userId.Value;

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Content("ModelState inválido:\n" + string.Join("\n", erros));
            }

            _context.PerfilTalentos.Add(perfil);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "BemVindoPerfil");


        }

        
        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null)
                return RedirectToAction("Create");

            return View(perfil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PerfilTalento perfilAtualizado)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var perfil = _context.PerfilTalentos.FirstOrDefault(p => p.IdUtilizador == userId);
            if (perfil == null)
                return RedirectToAction("Create");

            if (ModelState.IsValid)
            {
                perfil.Nome = perfilAtualizado.Nome;
                perfil.Pais = perfilAtualizado.Pais;
                perfil.Email = perfilAtualizado.Email;
                perfil.PrecoHora = perfilAtualizado.PrecoHora;
                perfil.Tipo = perfilAtualizado.Tipo;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perfilAtualizado);
        }
    }
}
