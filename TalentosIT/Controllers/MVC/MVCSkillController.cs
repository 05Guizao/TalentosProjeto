using Microsoft.AspNetCore.Mvc;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCSkillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCSkillController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var skills = _context.Skills.ToList();
            return View(skills);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.Estado = "Ativo"; // Definindo estado padrão
                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        // Podemos adicionar Edit e Delete depois
    }
}