using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCTalentoSkillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCTalentoSkillController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCTalento");

            var skills = _context.TalentoSkills
                .Include(ts => ts.Skill)
                .Where(ts => ts.CodPerfilTalento == perfil.Cod)
                .ToList();

            return View(skills);
        }

        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCTalento");

            ViewBag.Skills = _context.Skills.Where(s => s.Estado == "Ativo").ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CodSkill, int AnosDeExperiencia)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCTalento");

            var exists = await _context.TalentoSkills.AnyAsync(t => t.CodPerfilTalento == perfil.Cod && t.CodSkill == CodSkill);
            if (exists)
            {
                ModelState.AddModelError("", "Skill já associada.");
                ViewBag.Skills = _context.Skills.Where(s => s.Estado == "Ativo").ToList();
                return View();
            }

            _context.TalentoSkills.Add(new TalentoSkill
            {
                CodPerfilTalento = perfil.Cod,
                CodSkill = CodSkill,
                AnosDeExperiencia = AnosDeExperiencia
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int codSkill)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCTalento");

            var talentoSkill = await _context.TalentoSkills.FirstOrDefaultAsync(ts => ts.CodPerfilTalento == perfil.Cod && ts.CodSkill == codSkill);
            if (talentoSkill != null)
            {
                _context.TalentoSkills.Remove(talentoSkill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
