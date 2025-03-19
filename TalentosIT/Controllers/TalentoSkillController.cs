using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TalentoSkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TalentoSkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TalentoSkill>>> GetTalentoSkills()
    {
        return await _context.TalentoSkills.ToListAsync();
    }

    [HttpGet("{codPerfilTalento}/{codSkill}")]
    public async Task<ActionResult<TalentoSkill>> GetTalentoSkill(int codPerfilTalento, int codSkill)
    {
        var talentoSkill = await _context.TalentoSkills.FindAsync(codPerfilTalento, codSkill);

        if (talentoSkill == null)
        {
            return NotFound();
        }

        return talentoSkill;
    }
}