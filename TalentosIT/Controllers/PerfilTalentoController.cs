using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Models;

[Route("api/[controller]")]
[ApiController]
public class PerfilTalentoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PerfilTalentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PerfilTalento>>> GetPerfisTalento()
    {
        return await _context.PerfilTalentos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PerfilTalento>> GetPerfilTalento(int id)
    {
        var perfilTalento = await _context.PerfilTalentos.FindAsync(id);

        if (perfilTalento == null)
        {
            return NotFound();
        }

        return perfilTalento;
    }
}