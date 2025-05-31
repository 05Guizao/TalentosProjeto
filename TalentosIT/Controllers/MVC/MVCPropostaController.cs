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


        // GET: /MVCProposta/SelecionarPerfil
        public async Task<IActionResult> SelecionarPerfil()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            var perfis = await _context.PerfilTalentos.ToListAsync();
            return View(perfis);
        }

// GET: /MVCProposta/Create?perfilId=3
        public IActionResult Create(int perfilId)
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            if (tipo != "Empresa")
                return RedirectToAction("Login", "Account");

            ViewBag.PerfilId = perfilId;
            return View();
        }

// POST: /MVCProposta/Create
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
                proposta.IdPerfilTalento = perfilId; // <- aqui associamos o perfil
                proposta.Estado = "Ativo";

                _context.Add(proposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(proposta);
        }



        // Edit & Delete mantêm-se como já estavam
    }
}
