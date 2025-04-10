using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Data;
using TalentosIT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentosIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilTalentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PerfilTalentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PerfilTalento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilTalento>>> GetPerfisTalento()
        {
            return await _context.PerfilTalentos.ToListAsync();
        }

        // GET: api/PerfilTalento/{id}
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

        // POST: api/PerfilTalento
        [HttpPost]
        public async Task<ActionResult<PerfilTalento>> PostPerfilTalento(PerfilTalento perfilTalento)
        {
            _context.PerfilTalentos.Add(perfilTalento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerfilTalento), new { id = perfilTalento.Cod }, perfilTalento);
        }

        // PUT: api/PerfilTalento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfilTalento(int id, PerfilTalento perfilTalento)
        {
            if (id != perfilTalento.Cod)
            {
                return BadRequest();
            }

            _context.Entry(perfilTalento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilTalentoExists(id))
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

        // DELETE: api/PerfilTalento/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfilTalento(int id)
        {
            var perfilTalento = await _context.PerfilTalentos.FindAsync(id);
            if (perfilTalento == null)
            {
                return NotFound();
            }

            _context.PerfilTalentos.Remove(perfilTalento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilTalentoExists(int id)
        {
            return _context.PerfilTalentos.Any(e => e.Cod == id);
        }
    }
}
