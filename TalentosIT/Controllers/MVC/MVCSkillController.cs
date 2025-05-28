using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC
{
    public class MVCSkillController : Controller
    {
        private readonly SessaoUtilizadorService _sessao;
        private readonly SkillService _skillService;

        public MVCSkillController(SessaoUtilizadorService sessao, SkillService skillService)
        {
            _sessao = sessao;
            _skillService = skillService;
        }

        private bool EhAdmin()
        {
            var tipo = _sessao.ObterTipoUtilizador();
            return tipo == "Admin";
        }

        public IActionResult Index()
        {
            if (!EhAdmin())
                return Unauthorized();

            var skills = _skillService.ObterSkillsDoUtilizador(null); // Skills globais
            return View(skills);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!EhAdmin())
                return Unauthorized();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill model)
        {
            if (!EhAdmin())
                return Unauthorized();

            model.Estado = "Ativo";
            model.IdUtilizador = null; // global

            if (ModelState.IsValid)
            {
                _skillService.CriarSkill(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int cod)
        {
            if (!EhAdmin())
                return Unauthorized();

            var skill = _skillService.ObterSkill(cod, null);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill model)
        {
            if (!EhAdmin())
                return Unauthorized();

            var skillExistente = _skillService.ObterSkill(model.Cod, null);
            if (skillExistente == null)
                return NotFound();

            skillExistente.Nome = model.Nome;
            skillExistente.AreaProfissional = model.AreaProfissional;

            if (ModelState.IsValid)
            {
                _skillService.AtualizarSkill(skillExistente);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int cod)
        {
            if (!EhAdmin())
                return Unauthorized();

            _skillService.RemoverSkill(cod, null);
            return RedirectToAction(nameof(Index));
        }
    }
}
