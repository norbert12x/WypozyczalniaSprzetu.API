using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Data;
using WypozyczalniaSprzetu.API.Models;

namespace WypozyczalniaSprzetu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZwrotyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ZwrotyController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zwrot>>> GetZwroty()
            => await _context.Zwroty.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Zwrot>> GetZwrot(int id)
        {
            var z = await _context.Zwroty.FindAsync(id);
            return z is null ? NotFound() : Ok(z);
        }

        [HttpPost]
        public async Task<ActionResult<Zwrot>> PostZwrot(Zwrot z)
        {
            // obliczenie kary za spóźnienie
            var rez = await _context.Rezerwacje.FindAsync(z.RezerwacjaId);
            if (rez is null) return BadRequest("Brak rezerwacji o podanym ID.");
            z.CzySpóźnione = z.DataZwrotu > rez.DataDo;
            z.Kara = z.CzySpóźnione ? (decimal)((z.DataZwrotu - rez.DataDo).Days * 20.0) : 0m;
            _context.Zwroty.Add(z);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetZwrot), new { id = z.Id }, z);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZwrot(int id)
        {
            var z = await _context.Zwroty.FindAsync(id);
            if (z is null) return NotFound();
            _context.Zwroty.Remove(z);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
