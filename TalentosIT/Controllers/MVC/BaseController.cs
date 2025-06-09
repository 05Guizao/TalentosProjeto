using Microsoft.AspNetCore.Mvc;

namespace TalentosIT.Controllers
{
    public class BaseController : Controller
    {
        protected void SetUserInViewBag()
        {
            ViewBag.Nome = HttpContext.Session.GetString("UserNome");
            ViewBag.Tipo = HttpContext.Session.GetString("UserTipo");
        }
    }
}