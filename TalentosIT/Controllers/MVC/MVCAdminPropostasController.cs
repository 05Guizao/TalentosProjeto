using Microsoft.AspNetCore.Mvc;
using TalentosIT.Models;
using TalentosIT.Services;

namespace TalentosIT.Controllers.MVC
{
    public class AdminPropostasController : Controller
    {
        private readonly PropostaTrabalhoService _propostaService;

        public AdminPropostasController(PropostaTrabalhoService propostaService)
        {
            _propostaService = propostaService;
        }

        public async Task<IActionResult> Index()
        {
            var propostas = await _propostaService.ObterTodasAsync();
            return View(propostas);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProposta(PropostaTrabalho proposta)
        {
            var propostaOriginal = await _propostaService.ObterPorIdAsync(proposta.Id);

            if (propostaOriginal == null)
            {
                TempData["Erro"] = "Proposta não encontrada.";
                return RedirectToAction("Index");
            }

            if (propostaOriginal.Estado == "Sem Resposta")
            {
                TempData["Erro"] = "Só é possível editar propostas Aceites ou Recusadas.";
                return RedirectToAction("Index");
            }

            // Atualizar apenas campos permitidos
            propostaOriginal.Nome = proposta.Nome;
            propostaOriginal.CategoriaTalento = proposta.CategoriaTalento;
            propostaOriginal.NumTotalHoras = proposta.NumTotalHoras;
            propostaOriginal.DescricaoTrabalho = proposta.DescricaoTrabalho;
            propostaOriginal.Valor = proposta.Valor;

            await _propostaService.AtualizarAsync(propostaOriginal);

            TempData["Sucesso"] = "Proposta editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ApagarProposta(int id)
        {
            var proposta = await _propostaService.ObterPorIdAsync(id);

            if (proposta == null)
            {
                TempData["Erro"] = "Proposta não encontrada.";
                return RedirectToAction("Index");
            }

            if (proposta.Estado == "Sem Resposta")
            {
                TempData["Erro"] = "Só é possível apagar propostas Aceites ou Recusadas.";
                return RedirectToAction("Index");
            }

            await _propostaService.ApagarAsync(id);

            TempData["Sucesso"] = "Proposta apagada com sucesso.";
            return RedirectToAction("Index");
        }
    }
}
