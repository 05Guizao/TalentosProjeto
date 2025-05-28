using Microsoft.AspNetCore.Http;
using TalentosIT.Data;
using TalentosIT.Models;

namespace TalentosIT.Services
{
    public class SessaoUtilizadorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public SessaoUtilizadorService(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public int? ObterIdUtilizador()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
        }

        public Utilizador ObterUtilizador()
        {
            var userId = ObterIdUtilizador();
            if (userId == null)
                return null;

            return _context.Utilizadores
                .Find(userId.Value);
        }

        public string ObterTipoUtilizador()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserTipo");
        }
    }
}