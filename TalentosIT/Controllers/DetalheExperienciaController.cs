using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TalentosIT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetalheExperienciaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DetalheExperienciaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalheExperiencia>>> GetDetalhesExperiencia()
    {
        return await _context.DetalheExperiencias.ToListAsync();
    }

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
}