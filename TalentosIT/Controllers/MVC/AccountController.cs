using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Por favor preencha o email e a palavra-passe.";
                return View();
            }

            email = email.Trim();
            password = password.Trim();

            var utilizador = _context.Utilizadores
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (utilizador != null)
            {
                // Armazenar dados da sessão
                HttpContext.Session.SetInt32("UserId", utilizador.Id);
                HttpContext.Session.SetString("UserTipo", utilizador.Tipo.Trim());
                HttpContext.Session.SetString("UserNome", utilizador.Nome.Trim());

                // Redirecionamento baseado no tipo
                switch (utilizador.Tipo.Trim())
                {
                    case "Admin":
                        return RedirectToAction("Index", "MVCAdmin"); // Admin dashboard
                    case "Empresa":
                        return RedirectToAction("BemVindo", "MVCProposta"); // Empresa página inicial
                    case "Cliente":
                        return RedirectToAction("Index", "BemVindoPerfil"); // Cliente dashboard
                    default:
                        break;
                }
            }

            // Login inválido
            ViewBag.Error = "Email ou palavra-passe inválidos.";
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
            if (string.IsNullOrWhiteSpace(utilizador.Email) || string.IsNullOrWhiteSpace(utilizador.Password))
            {
                ViewBag.Error = "Email e palavra-passe são obrigatórios.";
                return View(utilizador);
            }

            utilizador.Email = utilizador.Email.Trim();
            utilizador.Password = utilizador.Password.Trim();

            var emailExiste = _context.Utilizadores.Any(u => u.Email == utilizador.Email);
            if (emailExiste)
            {
                ViewBag.Error = "Já existe uma conta com este email.";
                return View(utilizador);
            }

            _context.Utilizadores.Add(utilizador);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
