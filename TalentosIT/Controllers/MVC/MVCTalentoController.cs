using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    public class MVCTalentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCTalentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var talentos = await _context.PerfilTalentos.ToListAsync();
            return View(talentos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerfilTalento talento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talento);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var talento = await _context.PerfilTalentos.FindAsync(id);
            if (talento == null)
                return NotFound();

            return View(talento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PerfilTalento talento)
        {
            if (id != talento.Cod) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(talento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talento);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var talento = await _context.PerfilTalentos.FindAsync(id);
            if (talento == null) return NotFound();

            _context.PerfilTalentos.Remove(talento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
