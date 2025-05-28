using Microsoft.AspNetCore.Mvc;
using TalentosIT.Data;

namespace TalentosIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TesteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var utilizadores = _context.Utilizadores.Take(1).ToList();
                return Ok(utilizadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro na ligação: " + ex.Message);
            }
        }
        
        [HttpGet("/test-perfis")]
        public IActionResult TestPerfis()
        {
            var perfis = _context.PerfilTalentos.Take(5).ToList();
            return Ok(perfis);
        }
    }
}