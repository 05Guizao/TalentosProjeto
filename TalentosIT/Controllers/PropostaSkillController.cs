using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PropostaSkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PropostaSkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropostaSkill>>> GetPropostaSkills()
    {
        return await _context.PropostaSkills.ToListAsync();
    }

    [HttpGet("{idPropostaTrabalho}/{codSkill}")]
    public async Task<ActionResult<PropostaSkill>> GetPropostaSkill(int idPropostaTrabalho, int codSkill)
    {
        var propostaSkill = await _context.PropostaSkills.FindAsync(idPropostaTrabalho, codSkill);

        if (propostaSkill == null)
        {
            return NotFound();
        }

        return propostaSkill;
    }
}