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
                .Include(p => p.PerfilTalento)
                .ToListAsync();

            return View(propostas);
        }

        [HttpGet]
        public IActionResult BemVindo()
        {
            return View();
        }

        public async Task<IActionResult> SelecionarPerfil()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var perfis = await _context.PerfilTalentos.ToListAsync();
            return View(perfis);
        }

        public IActionResult Create(int perfilId)
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

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

            if (ModelState.IsValid)
            {
                proposta.IdUtilizador = userId.Value;
                proposta.IdPerfilTalento = perfilId;
                proposta.Estado = "Ativo";

                _context.Add(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PerfilId = perfilId;
            return View(proposta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);

            if (proposta == null || proposta.IdUtilizador != userId)
                return NotFound();

            return View(proposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropostaTrabalho proposta)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (id != proposta.Id || userId == null)
                return NotFound();

            var propostaExistente = await _context.PropostaTrabalhos.FindAsync(id);
            if (propostaExistente == null || propostaExistente.IdUtilizador != userId)
                return NotFound();

            if (ModelState.IsValid)
            {
                proposta.IdUtilizador = userId.Value;
                proposta.IdPerfilTalento = propostaExistente.IdPerfilTalento;

                _context.Entry(propostaExistente).CurrentValues.SetValues(proposta);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(proposta);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var proposta = await _context.PropostaTrabalhos
                .FirstOrDefaultAsync(p => p.Id == id && p.IdUtilizador == userId);

            if (proposta == null)
                return NotFound();

            return View(proposta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var proposta = await _context.PropostaTrabalhos
                .FirstOrDefaultAsync(p => p.Id == id && p.IdUtilizador == userId);

            if (proposta != null)
            {
                _context.PropostaTrabalhos.Remove(proposta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
