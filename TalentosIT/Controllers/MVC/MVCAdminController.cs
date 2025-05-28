using Microsoft.AspNetCore.Mvc;

namespace TalentosIT.Controllers
{
    public class MVCAdminController : Controller
    {
        public IActionResult Index()
        {
            return View(); // A view será a que você criou: Views/MVCAdmin/Index.cshtml
        }
    }
}