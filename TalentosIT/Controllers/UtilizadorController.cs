using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class UtilizadorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UtilizadorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Utilizador
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilizador>>> GetUtilizadores()
    {
        return await _context.Utilizadores.ToListAsync();
    }

    // GET: api/Utilizador/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Utilizador>> GetUtilizador(int id)
    {
        var utilizador = await _context.Utilizadores.FindAsync(id);

        if (utilizador == null)
        {
            return NotFound();
        }

        return utilizador;
    }

    // POST: api/Utilizador
    [HttpPost]
    public async Task<ActionResult<Utilizador>> PostUtilizador(Utilizador utilizador)
    {
        _context.Utilizadores.Add(utilizador);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUtilizador), new { id = utilizador.Id }, utilizador);
    }

    // PUT: api/Utilizador/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUtilizador(int id, Utilizador utilizador)
    {
        if (id != utilizador.Id)
        {
            return BadRequest();
        }

        _context.Entry(utilizador).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UtilizadorExists(id))
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

    // DELETE: api/Utilizador/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUtilizador(int id)
    {
        var utilizador = await _context.Utilizadores.FindAsync(id);
        if (utilizador == null)
        {
            return NotFound();
        }

        _context.Utilizadores.Remove(utilizador);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UtilizadorExists(int id)
    {
        return _context.Utilizadores.Any(e => e.Id == id);
    }
}
