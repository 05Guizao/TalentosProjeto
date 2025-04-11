using Microsoft.AspNetCore.Mvc;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Linq;

namespace TalentosIT.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            email = email?.Trim();
            password = password?.Trim();

            var utilizador = _context.Utilizadores
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (utilizador != null)
            {
                return RedirectToAction("BemVindo", "Home");
            }

            ViewBag.Error = "Email ou password inválidos.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Utilizador utilizador)
        {
            utilizador.Email = utilizador.Email?.Trim();

            var emailExiste = _context.Utilizadores.Any(u => u.Email == utilizador.Email);
            if (emailExiste)
            {
                ViewBag.Error = "Já existe uma conta com este email.";
                return View(utilizador);
            }

            // Tipo já vem do formulário (Cliente ou Empresa)
            _context.Utilizadores.Add(utilizador);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}