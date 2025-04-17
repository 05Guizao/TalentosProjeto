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
    public class PropostaSkillController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropostaSkillController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PropostaSkill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropostaSkill>>> GetPropostaSkills()
        {
            return await _context.PropostaSkills.ToListAsync();
        }

        // GET: api/PropostaSkill/{idPropostaTrabalho}/{codSkill}
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

        // PUT: api/PropostaSkill/{idPropostaTrabalho}/{codSkill}
        [HttpPut("{idPropostaTrabalho}/{codSkill}")]
        public async Task<IActionResult> PutPropostaSkill(int idPropostaTrabalho, int codSkill, PropostaSkill propostaSkill)
        {
            if (idPropostaTrabalho != propostaSkill.IdPropostaTrabalho || codSkill != propostaSkill.CodSkill)
            {
                return BadRequest("Os IDs fornecidos na URL não correspondem aos IDs no corpo da requisição.");
            }

            _context.Entry(propostaSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PropostaSkills.Any(e => e.IdPropostaTrabalho == idPropostaTrabalho && e.CodSkill == codSkill))
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

        // POST: api/PropostaSkill
        [HttpPost]
        public async Task<ActionResult<PropostaSkill>> PostPropostaSkill(PropostaSkill propostaSkill)
        {
            _context.PropostaSkills.Add(propostaSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPropostaSkill),
                new { idPropostaTrabalho = propostaSkill.IdPropostaTrabalho, codSkill = propostaSkill.CodSkill },
                propostaSkill);
        }

        // DELETE: api/PropostaSkill/{idPropostaTrabalho}/{codSkill}
        [HttpDelete("{idPropostaTrabalho}/{codSkill}")]
        public async Task<IActionResult> DeletePropostaSkill(int idPropostaTrabalho, int codSkill)
        {
            var propostaSkill = await _context.PropostaSkills.FindAsync(idPropostaTrabalho, codSkill);
            if (propostaSkill == null)
            {
                return NotFound();
            }

            _context.PropostaSkills.Remove(propostaSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
