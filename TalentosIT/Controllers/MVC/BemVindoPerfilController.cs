using Microsoft.AspNetCore.Mvc;

namespace TalentosIT.Controllers
{
    public class BemVindoPerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}