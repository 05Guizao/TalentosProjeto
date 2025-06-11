using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using X.PagedList;



namespace TalentosIT.Controllers.MVC
{
    public class MVCPropostaClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCPropostaClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cliente vê as propostas recebidas com paginação e filtro
        public async Task<IActionResult> MinhasPropostas(int? page, string estado)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var tipo = HttpContext.Session.GetString("UserTipo");

            if (userId == null || tipo != "Cliente")
                return RedirectToAction("Login", "Account");

            var perfil = await _context.PerfilTalentos
                .FirstOrDefaultAsync(p => p.IdUtilizador == userId.Value);

            if (perfil == null)
                return RedirectToAction("Create", "MVCPerfilTalento");

            var query = _context.PropostaTrabalhos
                .Where(p => p.IdPerfilTalento == perfil.Cod);

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(p => p.Estado == estado);
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var propostas = query.OrderByDescending(p => p.Id).ToPagedList(pageNumber, pageSize);
            
            ViewBag.EstadoAtual = estado;
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

            if (proposta == null)
            {
                TempData["Mensagem"] = "Proposta não encontrada ou não pertence ao seu perfil.";
                return RedirectToAction(nameof(MinhasPropostas));
            }

            // DEBUG: ver estado real
            Console.WriteLine($"[DEBUG] Proposta ID: {id}");
            Console.WriteLine($"[DEBUG] Estado atual: '{proposta?.Estado}'");

            // Verificação robusta do estado atual
            if (!string.Equals(proposta.Estado?.Trim(), "Sem Resposta", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Mensagem"] = "Esta proposta já foi respondida anteriormente.";
                return RedirectToAction(nameof(MinhasPropostas));
            }

            // Atualização
            proposta.Estado = novoEstado;
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = $"Proposta foi {novoEstado.ToLower()} com sucesso.";
            return RedirectToAction(nameof(MinhasPropostas));
        }
    }
}
