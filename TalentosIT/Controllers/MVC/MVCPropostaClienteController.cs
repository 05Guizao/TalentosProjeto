using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;

namespace TalentosIT.Controllers.MVC
{
    public class MVCPropostaClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCPropostaClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MinhasPropostas()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos
                .FirstOrDefaultAsync(p => p.IdUtilizador == userId.Value);

            if (perfil == null)
                return RedirectToAction("Create", "MVCPerfilTalento");

            var propostas = await _context.PropostaTrabalhos
                .Where(p => p.IdPerfilTalento == perfil.Cod)
                .Include(p => p.PerfilTalento)
                .ToListAsync();

            return View(propostas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEstado(int id, string novoEstado)
        {
            var proposta = await _context.PropostaTrabalhos.FindAsync(id);
            if (proposta == null) return NotFound();

            proposta.Estado = novoEstado;
            await _context.SaveChangesAsync();

            return RedirectToAction("MinhasPropostas");
        }
    }
}