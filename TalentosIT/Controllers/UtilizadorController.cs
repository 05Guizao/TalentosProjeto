using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Models;

[Route("api/[controller]")]
[ApiController]
public class UtilizadorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UtilizadorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilizador>>> GetUtilizadores()
    {
        return await _context.Utilizadores.ToListAsync();
    }
}