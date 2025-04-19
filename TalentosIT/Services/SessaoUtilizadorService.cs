using Microsoft.AspNetCore.Http;

namespace TalentosIT.Services
{
    public class SessaoUtilizadorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessaoUtilizadorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? ObterIdUtilizador()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
        }
    }
}