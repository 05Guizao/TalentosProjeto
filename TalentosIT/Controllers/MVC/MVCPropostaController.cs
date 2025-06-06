using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCPropostaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCPropostaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BemVindo()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            return View();
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var propostas = await _context.PropostaTrabalhos
                .Where(p => p.IdUtilizador == userId)
                .Include(p => p.PerfilTalento)
                .ToListAsync();

            return View(propostas);
        }

        public async Task<IActionResult> SelecionarPerfil()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var perfis = await _context.PerfilTalentos
                .Include(p => p.TalentoSkills)
                .ThenInclude(ts => ts.Skill)
                .ToListAsync();

            return View(perfis);
        }

        public async Task<IActionResult> Create(int perfilId)
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            // Verificar se o perfil realmente existe
            var perfil = await _context.PerfilTalentos.FindAsync(perfilId);
            if (perfil == null)
                return NotFound("Perfil não encontrado.");

            ViewBag.PerfilId = perfilId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropostaTrabalho proposta, int perfilId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            // Verificar se o perfil ainda existe
            var perfil = await _context.PerfilTalentos.FindAsync(perfilId);
            if (perfil == null)
            {
                ModelState.AddModelError("", "O perfil selecionado já não existe.");
                ViewBag.PerfilId = perfilId;
                return View(proposta);
            }

            if (ModelState.IsValid)
            {
                proposta.IdUtilizador = userId.Value;
                proposta.IdPerfilTalento = perfilId;
                proposta.Estado = "Pendente";

                _context.Add(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PerfilId = perfilId;
            return View(proposta);
        }
    }
}
