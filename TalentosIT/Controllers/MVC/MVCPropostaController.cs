using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var propostas = await _context.PropostaTrabalhos
                .Where(p => p.IdUtilizador == userId)
                .ToListAsync();

            return View(propostas);
        }

        public IActionResult Create()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropostaTrabalho proposta)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                proposta.IdUtilizador = userId.Value;
                proposta.Estado = "Ativo";
                _context.Add(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(proposta);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);
            var userId = HttpContext.Session.GetInt32("UserId");

            if (proposta == null || proposta.IdUtilizador != userId)
                return NotFound();

            return View(proposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropostaTrabalho proposta)
        {
            if (id != proposta.Id)
                return NotFound();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (proposta.IdUtilizador != userId)
                return Unauthorized();

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
            var userId = HttpContext.Session.GetInt32("UserId");

            if (proposta == null || proposta.IdUtilizador != userId)
                return NotFound();

            _context.PropostaTrabalhos.Remove(proposta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
