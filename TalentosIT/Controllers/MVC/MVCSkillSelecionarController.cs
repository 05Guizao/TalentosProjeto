using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TalentosIT.Data;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC
{
    public class MVCSkillSelecionarController : Controller
    {
        private readonly SessaoUtilizadorService _sessao;
        private readonly ApplicationDbContext _context;

        public MVCSkillSelecionarController(SessaoUtilizadorService sessao, ApplicationDbContext context)
        {
            _sessao = sessao;
            _context = context;
        }

        public IActionResult Index()
        {
            var idUtilizador = _sessao.ObterIdUtilizador();
            if (idUtilizador == null)
                return RedirectToAction("Login", "Account");

            // Listar apenas as skills globais criadas por administradores (sem id de utilizador associado)
            var skillsGlobais = _context.Skills.Where(s => s.IdUtilizador == null).ToList();
            return View(skillsGlobais);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Selecionar(List<int> skillIds)
        {
            var idUtilizador = _sessao.ObterIdUtilizador();
            if (idUtilizador == null)
                return RedirectToAction("Login", "Account");

            foreach (var id in skillIds)
            {
                var skillGlobal = _context.Skills.FirstOrDefault(s => s.Cod == id && s.IdUtilizador == null);
                if (skillGlobal != null)
                {
                    var novaSkill = new Skill
                    {
                        Nome = skillGlobal.Nome,
                        AreaProfissional = skillGlobal.AreaProfissional,
                        Estado = "Selecionada",
                        IdUtilizador = idUtilizador.Value
                    };
                    _context.Skills.Add(novaSkill);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
