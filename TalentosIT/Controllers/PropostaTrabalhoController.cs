using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentosIT.Models;

[Route("api/[controller]")]
[ApiController]
public class PropostaTrabalhoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PropostaTrabalhoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/PropostaTrabalho
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropostaTrabalho>>> GetPropostasTrabalho()
    {
        return await _context.PropostaTrabalhos.ToListAsync();
    }

    // GET: api/PropostaTrabalho/{id}
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

    // POST: api/PropostaTrabalho
    [HttpPost]
    public async Task<ActionResult<PropostaTrabalho>> PostPropostaTrabalho(PropostaTrabalho propostaTrabalho)
    {
        _context.PropostaTrabalhos.Add(propostaTrabalho);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPropostaTrabalho), new { id = propostaTrabalho.Id }, propostaTrabalho);
    }

    // PUT: api/PropostaTrabalho/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPropostaTrabalho(int id, PropostaTrabalho propostaTrabalho)
    {
        if (id != propostaTrabalho.Id)
        {
            return BadRequest();
        }

        _context.Entry(propostaTrabalho).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PropostaTrabalhoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/PropostaTrabalho/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropostaTrabalho(int id)
    {
        var propostaTrabalho = await _context.PropostaTrabalhos.FindAsync(id);
        if (propostaTrabalho == null)
        {
            return NotFound();
        }

        _context.PropostaTrabalhos.Remove(propostaTrabalho);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PropostaTrabalhoExists(int id)
    {
        return _context.PropostaTrabalhos.Any(e => e.Id == id);
    }
}
