using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TalentosIT.Data;
using TalentosIT.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TalentosIT.Documents;
using System.IO;

namespace TalentosIT.Controllers
{
    public class MVCRelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MVCRelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<RelatorioViewModel> GerarRelatorioAsync()
        {
            var precoCategoriaPais = await _context.PropostaTrabalhos
                .Where(p => p.NumTotalHoras > 0 && p.Valor > 0 && p.PerfilTalento != null)
                .Include(p => p.PerfilTalento)
                .ToListAsync();

            var agrupadoCategoriaPais = precoCategoriaPais
                .GroupBy(p => new { p.CategoriaTalento, p.PerfilTalento.Pais })
                .Select(g => new RelatorioCategoriaPaisViewModel
                {
                    Categoria = g.Key.CategoriaTalento,
                    Pais = g.Key.Pais,
                    PrecoMedio = g.Average(p => (decimal)(p.Valor / p.NumTotalHoras * 176))
                })
                .ToList();

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

            return new RelatorioViewModel
            {
                PorCategoriaPais = agrupadoCategoriaPais,
                PorSkill = precoSkill
            };
        }

        public async Task<IActionResult> Index()
        {
            var tipo = HttpContext.Session.GetString("UserTipo");
            var nome = HttpContext.Session.GetString("UserNome");

            if (tipo != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.Nome = nome;
            ViewBag.Tipo = tipo;

            var model = await GerarRelatorioAsync();
            return View(model);
        }

        public async Task<IActionResult> ExportarPDF()
        {
            var model = await GerarRelatorioAsync();
            var document = new RelatorioPdfDocument(model);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "RelatorioTalentosIT.pdf");
        }
    }
}
