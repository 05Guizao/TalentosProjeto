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
    public class TalentoSkillController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TalentoSkillController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TalentoSkill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TalentoSkill>>> GetTalentoSkills()
        {
            return await _context.TalentoSkills.ToListAsync();
        }

        // GET: api/TalentoSkill/{codPerfilTalento}/{codSkill}
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

        // PUT: api/TalentoSkill/{codPerfilTalento}/{codSkill}
        [HttpPut("{codPerfilTalento}/{codSkill}")]
        public async Task<IActionResult> PutTalentoSkill(int codPerfilTalento, int codSkill, TalentoSkill talentoSkill)
        {
            if (codPerfilTalento != talentoSkill.CodPerfilTalento || codSkill != talentoSkill.CodSkill)
            {
                return BadRequest("Os IDs fornecidos na URL não correspondem aos IDs no corpo da requisição.");
            }

            _context.Entry(talentoSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TalentoSkills.Any(e => e.CodPerfilTalento == codPerfilTalento && e.CodSkill == codSkill))
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

        // POST: api/TalentoSkill
        [HttpPost]
        public async Task<ActionResult<TalentoSkill>> PostTalentoSkill(TalentoSkill talentoSkill)
        {
            _context.TalentoSkills.Add(talentoSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTalentoSkill),
                new { codPerfilTalento = talentoSkill.CodPerfilTalento, codSkill = talentoSkill.CodSkill },
                talentoSkill);
        }

        // DELETE: api/TalentoSkill/{codPerfilTalento}/{codSkill}
        [HttpDelete("{codPerfilTalento}/{codSkill}")]
        public async Task<IActionResult> DeleteTalentoSkill(int codPerfilTalento, int codSkill)
        {
            var talentoSkill = await _context.TalentoSkills.FindAsync(codPerfilTalento, codSkill);
            if (talentoSkill == null)
            {
                return NotFound();
            }

            _context.TalentoSkills.Remove(talentoSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
