using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaSprzetu.API.Data;
using WypozyczalniaSprzetu.API.Models;

namespace WypozyczalniaSprzetu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategorieSprzetuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public KategorieSprzetuController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KategoriaSprzetu>>> GetKategorie()
            => await _context.KategorieSprzetu.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<KategoriaSprzetu>> GetKategoria(int id)
        {
            var kat = await _context.KategorieSprzetu.FindAsync(id);
            return kat is null ? NotFound() : Ok(kat);
        }

        [HttpPost]
        public async Task<ActionResult<KategoriaSprzetu>> PostKategoria(KategoriaSprzetu kategoria)
        {
            _context.KategorieSprzetu.Add(kategoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKategoria), new { id = kategoria.Id }, kategoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategoria(int id, KategoriaSprzetu kategoria)
        {
            if (id != kategoria.Id) return BadRequest();
            _context.Entry(kategoria).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.KategorieSprzetu.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategoria(int id)
        {
            var kat = await _context.KategorieSprzetu.FindAsync(id);
            if (kat is null) return NotFound();
            _context.KategorieSprzetu.Remove(kat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
