using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    

namespace TalentosIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalheExperienciaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalheExperienciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalheExperiencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalheExperiencia>>> GetDetalhesExperiencia()
        {
            return await _context.DetalheExperiencias.ToListAsync();
        }

        // GET: api/DetalheExperiencia/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalheExperiencia>> GetDetalheExperiencia(int id)
        {
            var detalheExperiencia = await _context.DetalheExperiencias.FindAsync(id);

            if (detalheExperiencia == null)
            {
                return NotFound();
            }

            return detalheExperiencia;
        }

        // POST: api/DetalheExperiencia
        [HttpPost]
        public async Task<ActionResult<DetalheExperiencia>> PostDetalheExperiencia(DetalheExperiencia detalheExperiencia)
        {
            _context.DetalheExperiencias.Add(detalheExperiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalheExperiencia), new { id = detalheExperiencia.CodExperienciaTalento }, detalheExperiencia);
        }

        // PUT: api/DetalheExperiencia/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalheExperiencia(int id, DetalheExperiencia detalheExperiencia)
        {
            if (id != detalheExperiencia.CodExperienciaTalento)
            {
                return BadRequest();
            }

            _context.Entry(detalheExperiencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalheExperienciaExists(id))
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

        // DELETE: api/DetalheExperiencia/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalheExperiencia(int id)
        {
            var detalheExperiencia = await _context.DetalheExperiencias.FindAsync(id);
            if (detalheExperiencia == null)
            {
                return NotFound();
            }

            _context.DetalheExperiencias.Remove(detalheExperiencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalheExperienciaExists(int id)
        {
            return _context.DetalheExperiencias.Any(e => e.CodExperienciaTalento == id);
        }
    }
}
