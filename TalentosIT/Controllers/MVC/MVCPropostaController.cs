using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
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

        public async Task<IActionResult> Index()
        {
            var propostas = await _context.PropostaTrabalhos.ToListAsync();
            return View(propostas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropostaTrabalho proposta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proposta);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);
            if (proposta == null)
                return NotFound();

            return View(proposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropostaTrabalho proposta)
        {
            if (id != proposta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proposta);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);
            if (proposta == null) return NotFound();

            _context.PropostaTrabalhos.Remove(proposta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
