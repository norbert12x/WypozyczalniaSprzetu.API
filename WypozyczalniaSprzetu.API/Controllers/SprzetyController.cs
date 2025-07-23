using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Data;
using WypozyczalniaSprzetu.API.Models;

[Route("api/[controller]")]
[ApiController]
public class SprzetyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SprzetyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sprzet>>> GetSprzety()
    {
        return await _context.Sprzety.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sprzet>> GetSprzet(int id)
    {
        var sprzet = await _context.Sprzety.FindAsync(id);

        if (sprzet == null)
        {
            return NotFound();
        }

        return sprzet;
    }

    [HttpPost]
    public async Task<ActionResult<Sprzet>> PostSprzet(Sprzet sprzet)
    {
        _context.Sprzety.Add(sprzet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSprzet), new { id = sprzet.Id }, sprzet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSprzet(int id, Sprzet sprzet)
    {
        if (id != sprzet.Id)
        {
            return BadRequest();
        }

        _context.Entry(sprzet).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSprzet(int id)
    {
        var sprzet = await _context.Sprzety.FindAsync(id);
        if (sprzet == null)
        {
            return NotFound();
        }

        _context.Sprzety.Remove(sprzet);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
