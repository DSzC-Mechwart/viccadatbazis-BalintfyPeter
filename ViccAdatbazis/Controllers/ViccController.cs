using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        //Adatbázis Kapcsolat
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }


        //Viccek Listázása
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv == true).ToListAsync();
        }


        // Egy Vicc Lekérdezése
        [HttpGet("{id}")]

        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);

            if (vicc == null)
            {
                return NotFound();
            }

            return vicc;
        }


        //Új Vicc Feltöltésae
        [HttpPost]

        public async Task<ActionResult> PostVicc(Vicc ujVicc)
        {
            _context.Viccek.Add(ujVicc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVicc", new { id = ujVicc.Id }, ujVicc);
        }


        //Vicc Módosaítása
        [HttpPut("{id}")]

        public async Task<ActionResult> PutVicc(int id, Vicc modositottVicc)
        {
            if (id != modositottVicc.Id)
            {
                return BadRequest();
            }

            _context.Entry(modositottVicc).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        //Vicc Törlése
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteVicc(int id)
        {
            var torlendoVicc = await _context.Viccek.FindAsync(id);

            if (torlendoVicc == null)
            {
                return NotFound();
            }

            else if (torlendoVicc.Aktiv)
            {
                torlendoVicc.Aktiv = false;
            }

            else
            {
                _context.Viccek.Remove(torlendoVicc);
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }

}
