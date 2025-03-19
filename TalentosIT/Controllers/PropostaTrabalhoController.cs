using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PropostaTrabalhoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PropostaTrabalhoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropostaTrabalho>>> GetPropostasTrabalho()
    {
        return await _context.PropostaTrabalhos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropostaTrabalho>> GetPropostaTrabalho(int id)
    {
        var propostaTrabalho = await _context.PropostaTrabalhos.FindAsync(id);

        if (propostaTrabalho == null)
        {
            return NotFound();
        }

        return propostaTrabalho;
    }
}