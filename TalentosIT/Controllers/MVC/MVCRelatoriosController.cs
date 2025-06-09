using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TalentosIT.Data;
using TalentosIT.ViewModels;

namespace TalentosIT.Controllers
{
    public class MVCRelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCRelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (tipo != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.Nome = nome;
            ViewBag.Tipo = tipo;

            // Relatório por categoria e país
            var precoCategoriaPais = await _context.PropostaTrabalhos
                .Where(p => p.NumTotalHoras > 0 && p.Valor > 0 && p.PerfilTalento != null)
                .Include(p => p.PerfilTalento)
                .ToListAsync(); // Processar em memória

            var agrupadoCategoriaPais = precoCategoriaPais
                .GroupBy(p => new { p.CategoriaTalento, p.PerfilTalento.Pais })
                .Select(g => new RelatorioCategoriaPaisViewModel
                {
                    Categoria = g.Key.CategoriaTalento,
                    Pais = g.Key.Pais,
                    PrecoMedio = g.Average(p => (decimal)(p.Valor / p.NumTotalHoras * 176))
                })
                .ToList();

            // Relatório por skill
            var talentoSkills = await _context.TalentoSkills
                .Include(ts => ts.Skill)
                .Include(ts => ts.PerfilTalento)
                    .ThenInclude(pt => pt.PropostasTrabalho)
                .ToListAsync();

            var precoSkill = talentoSkills
                .SelectMany(ts => ts.PerfilTalento.PropostasTrabalho
                    .Where(p => p.NumTotalHoras > 0 && p.Valor > 0)
                    .Select(p => new { SkillNome = ts.Skill.Nome, Proposta = p }))
                .GroupBy(x => x.SkillNome)
                .Select(g => new RelatorioSkillViewModel
                {
                    Skill = g.Key,
                    PrecoMedio = g.Average(x => (double)(x.Proposta.Valor / x.Proposta.NumTotalHoras * 176))
                })
                .ToList();

            var model = new RelatorioViewModel
            {
                PorCategoriaPais = agrupadoCategoriaPais,
                PorSkill = precoSkill
            };

            return View(model);
        }
    }
}
