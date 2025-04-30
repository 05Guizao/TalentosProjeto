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

        /// <summary>
        /// Retorna o ID do utilizador guardado na sessão, ou null se não existir.
        /// </summary>
        public int? ObterIdUtilizador()
        {
            return _httpContextAccessor.HttpContext?
                .Session.GetInt32("UserId");
        }

        /// <summary>
        /// Retorna o objeto Utilizador correspondente ao ID na sessão,
        /// ou null se não estiver autenticado.
        /// </summary>
        public Utilizador ObterUtilizador()
        {
            var userId = ObterIdUtilizador();
            if (userId == null)
                return null;

            return _context.Utilizadores
                .Find(userId.Value);
        }
    }
}