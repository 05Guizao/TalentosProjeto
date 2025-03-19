using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
    {
        return await _context.Skills.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Skill>> GetSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
            return NotFound();
        }

        return skill;
    }
}