using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Data;
using WypozyczalniaSprzetu.API.Models;

namespace WypozyczalniaSprzetu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RezerwacjeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RezerwacjeController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezerwacja>>> GetRezerwacje()
            => await _context.Rezerwacje.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Rezerwacja>> GetRezerwacja(int id)
        {
            var r = await _context.Rezerwacje.FindAsync(id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public async Task<ActionResult<Rezerwacja>> PostRezerwacja(Rezerwacja rez)
        {
            // (opcjonalnie: walidacje klienta, sprzętu, dostępności, cena, rabat)
            rez.DataRezerwacji = DateTime.Now;
            rez.Status = "Aktywna";
            _context.Rezerwacje.Add(rez);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRezerwacja), new { id = rez.Id }, rez);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezerwacja(int id, Rezerwacja rez)
        {
            if (id != rez.Id) return BadRequest();
            _context.Entry(rez).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Rezerwacje.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRezerwacja(int id)
        {
            var rez = await _context.Rezerwacje.FindAsync(id);
            if (rez is null) return NotFound();
            // anulowanie: dopuszczalne min. 2 dni przed DataOd
            if (DateTime.Now > rez.DataOd.AddDays(-2))
                return BadRequest("Rezerwację można anulować najpóźniej 2 dni przed.");
            rez.Status = "Anulowana";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
