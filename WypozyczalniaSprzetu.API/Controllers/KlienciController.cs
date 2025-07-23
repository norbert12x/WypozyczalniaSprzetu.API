using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Models;
using WypozyczalniaSprzetu.API.Data;
[ApiController]
[Route("api/[controller]")]
public class KlienciController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public KlienciController(ApplicationDbContext context)
    {
        _context = context;
    }
    private bool KlientExists(int id)
    {
        return _context.Klienci.Any(e => e.Id == id);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Klient>>> GetKlienci()
    {
        return await _context.Klienci.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Klient>> GetKlient(int id)
    {
        var klient = await _context.Klienci.FindAsync(id);
        if (klient == null)
            return NotFound();

        return klient;
    }
    [HttpPost]
    public async Task<IActionResult> AddKlient([FromBody] Klient klient)
    {
        if (klient == null)
            return BadRequest("Nieprawidłowe dane klienta.");

        // Nawigacja już zawsze pustą listą, bo w modelu mamy '= new List<Rezerwacja>()'
        _context.Klienci.Add(klient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(AddKlient), new { id = klient.Id }, klient);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> PutKlient(int id, Klient klient)
    {
        if (id != klient.Id)
        {
            return BadRequest();
        }

        _context.Entry(klient).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!KlientExists(id))
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


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKlient(int id)
    {
        var klient = await _context.Klienci.FindAsync(id);
        if (klient == null)
            return NotFound();

        _context.Klienci.Remove(klient);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
