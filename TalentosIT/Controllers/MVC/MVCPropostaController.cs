using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            var nome = HttpContext.Session.GetString("UserNome");

            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            ViewBag.Nome = nome ?? "";
            ViewBag.Tipo = tipo ?? "";

            return View();
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (userId == null || tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            ViewBag.Nome = nome ?? "";
            ViewBag.Tipo = tipo ?? "";

            var propostas = await _context.PropostaTrabalhos
                .Where(p => p.IdUtilizador == userId)
                .Include(p => p.PerfilTalento)
                .ToListAsync();

            return View(propostas);
        }

        public async Task<IActionResult> SelecionarPerfil([FromQuery] int? skillId)
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            ViewBag.Nome = nome ?? "";
            ViewBag.Tipo = tipo ?? "";

            var skills = await _context.Skills.ToListAsync();
            ViewBag.Skills = skills;
            ViewBag.SkillId = skillId;

            var perfisQuery = _context.PerfilTalentos
                .Include(p => p.TalentoSkills)
                .ThenInclude(ts => ts.Skill)
                .Include(p => p.Experiencias)
                .AsQueryable();

            if (skillId.HasValue)
            {
                perfisQuery = perfisQuery.Where(p =>
                    p.TalentoSkills.Any(ts => ts.CodSkill == skillId.Value));
            }

            var perfis = await perfisQuery.ToListAsync();
            return View(perfis);
        }

        public async Task<IActionResult> Create(int perfilId)
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos
                .Include(p => p.Experiencias)
                .FirstOrDefaultAsync(p => p.Cod == perfilId);

            if (perfil == null)
                return NotFound("Perfil não encontrado.");

            ViewBag.PerfilId = perfilId;
            ViewBag.Experiencias = perfil.Experiencias?.ToList() ?? new List<DetalheExperiencia>();

            // ✅ Garante que a toolbar aparece corretamente
            ViewBag.Nome = nome ?? HttpContext.Session.GetString("UserNome");
            ViewBag.Tipo = tipo ?? HttpContext.Session.GetString("UserTipo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropostaTrabalho proposta, int perfilId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (userId == null || tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FindAsync(perfilId);
            if (perfil == null)
            {
                ModelState.AddModelError("", "O perfil selecionado já não existe.");
                ViewBag.PerfilId = perfilId;
                ViewBag.Nome = nome ?? "";
                ViewBag.Tipo = tipo ?? "";
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
            ViewBag.Nome = nome ?? "";
            ViewBag.Tipo = tipo ?? "";
            return View(proposta);
        }
    }
}
