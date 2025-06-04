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
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            var skills = await _context.TalentoSkills
                .Include(ts => ts.Skill)
                .Where(ts => ts.CodPerfilTalento == perfil.Cod)
                .ToListAsync();

            return View(skills);
        }

        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            ViewBag.Skills = await _context.Skills
                .Where(s => s.Estado == "Ativo")
                .ToListAsync();

            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int codSkill)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            var talentoSkill = await _context.TalentoSkills
                .FirstOrDefaultAsync(ts => ts.CodPerfilTalento == perfil.Cod && ts.CodSkill == codSkill);

            if (talentoSkill == null)
            {
                return NotFound();
            }

            return View(talentoSkill);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CodSkill, int AnosDeExperiencia, string NivelConforto, string DescricaoProjetos, string AreaProfissional)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            var exists = await _context.TalentoSkills.AnyAsync(t => t.CodPerfilTalento == perfil.Cod && t.CodSkill == CodSkill);
            if (exists)
            {
                ModelState.AddModelError("", "Skill já associada.");
                ViewBag.Skills = await _context.Skills.Where(s => s.Estado == "Ativo").ToListAsync();
                return View();
            }

            _context.TalentoSkills.Add(new TalentoSkill
            {
                CodPerfilTalento = perfil.Cod,
                CodSkill = CodSkill,
                AnosDeExperiencia = AnosDeExperiencia,
                NivelConforto = NivelConforto,
                DescricaoProjetos = DescricaoProjetos,
                AreaProfissional = AreaProfissional // 🆕 NOVO
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
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            var talentoSkill = await _context.TalentoSkills.FirstOrDefaultAsync(ts => ts.CodPerfilTalento == perfil.Cod && ts.CodSkill == codSkill);
            if (talentoSkill != null)
            {
                _context.TalentoSkills.Remove(talentoSkill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TalentoSkill model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos.FirstOrDefaultAsync(p => p.IdUtilizador == userId);
            if (perfil == null) return RedirectToAction("Create", "MVCPerfilTalento");

            var talentoSkill = await _context.TalentoSkills
                .FirstOrDefaultAsync(ts => ts.CodPerfilTalento == perfil.Cod && ts.CodSkill == model.CodSkill);

            if (talentoSkill == null)
            {
                return NotFound();
            }

            // Atualizar os campos editáveis
            talentoSkill.AreaProfissional = model.AreaProfissional;
            talentoSkill.AnosDeExperiencia = model.AnosDeExperiencia;
            talentoSkill.NivelConforto = model.NivelConforto;
            talentoSkill.DescricaoProjetos = model.DescricaoProjetos;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
