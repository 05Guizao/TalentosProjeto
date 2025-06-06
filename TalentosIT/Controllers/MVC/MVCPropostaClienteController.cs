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

        // GET: Visualizar propostas recebidas pelo cliente
        public async Task<IActionResult> MinhasPropostas()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Cliente")
                return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos
                .FirstOrDefaultAsync(p => p.IdUtilizador == userId.Value);

            if (perfil == null)
                return RedirectToAction("Create", "MVCPerfilTalento");

            var propostas = await _context.PropostaTrabalhos
                .Where(p => p.IdPerfilTalento == perfil.Cod)
                .OrderByDescending(p => p.Id) // ordena por mais recente primeiro
                .ToListAsync();

            return View("MinhasPropostasCliente", propostas);
        }

        // POST: Cliente aceita ou recusa uma proposta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEstado(int id, string novoEstado)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Cliente")
                return RedirectToAction("Login", "Account");

            var proposta = await _context.PropostaTrabalhos
                .Include(p => p.PerfilTalento)
                .FirstOrDefaultAsync(p => p.Id == id && p.PerfilTalento.IdUtilizador == userId);

            if (proposta == null || proposta.Estado != "Pendente")
                return NotFound();

            proposta.Estado = novoEstado;
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = $"Proposta foi {novoEstado.ToLower()} com sucesso.";
            return RedirectToAction(nameof(MinhasPropostas));
        }
    }
}
