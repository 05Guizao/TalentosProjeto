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
            return _sessao.ObterTipoUtilizador() == "Admin";
        }

        public IActionResult Index()
        {
            if (!EhAdmin()) return Unauthorized();

            var skills = _skillService.ObterTodasSkillsGlobais();
            return View(skills);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!EhAdmin()) return Unauthorized();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill model)
        {
            if (!EhAdmin()) return Unauthorized();

            model.Estado = "Ativo";

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
            if (!EhAdmin()) return Unauthorized();

            var skill = _skillService.ObterSkill(cod);
            if (skill == null) return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill model)
        {
            if (!EhAdmin()) return Unauthorized();

            var skill = _skillService.ObterSkill(model.Cod);
            if (skill == null) return NotFound();

            skill.Nome = model.Nome;
            skill.AreaProfissional = model.AreaProfissional;

            if (ModelState.IsValid)
            {
                _skillService.AtualizarSkill(skill);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int cod)
        {
            if (!EhAdmin()) return Unauthorized();

            _skillService.RemoverSkill(cod);
            return RedirectToAction(nameof(Index));
        }
    }
}
