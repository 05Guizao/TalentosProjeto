using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC
{
    public class MVCSkillController : Controller
    {
        private readonly SessaoUtilizadorService _sessao;
        private readonly SkillService _skillService;

        public MVCSkillController(
            SessaoUtilizadorService sessao,
            SkillService skillService)
        {
            _sessao = sessao;
            _skillService = skillService;
        }

        public IActionResult Index()
        {
            var userId = _sessao.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var skills = _skillService.ObterSkillsDoUsuario(userId.Value);
            return View(skills);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (_sessao.ObterIdUtilizador() == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill model)
        {
            var userId = _sessao.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            model.Estado = "Ativo";
            model.IdUtilizador = userId.Value;

            if (ModelState.IsValid)
            {
                _skillService.CriarSkill(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = _sessao.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var skill = _skillService.ObterSkill(id, userId.Value);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill model)
        {
            var userId = _sessao.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var existing = _skillService.ObterSkill(model.Cod, userId.Value);
            if (existing == null)
                return NotFound();

            existing.Nome = model.Nome;
            existing.AreaProfissional = model.AreaProfissional;
            // Estado mantido ou atualizado conforme necessidade

            if (ModelState.IsValid)
            {
                _skillService.AtualizarSkill(existing);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = _sessao.ObterIdUtilizador();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            _skillService.RemoverSkill(id, userId.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}