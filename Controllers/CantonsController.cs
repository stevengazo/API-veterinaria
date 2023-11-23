using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DBContexts;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CantonsController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public CantonsController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/Cantons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canton>>> GetCantons()
        {
          if (_context.Cantons == null)
          {
              return NotFound();
          }
            return await _context.Cantons.ToListAsync();
        }

        // GET: api/Cantons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canton>> GetCanton(int id)
        {
          if (_context.Cantons == null)
          {
              return NotFound();
          }
            var canton = await _context.Cantons.Include(e => e.Province).FirstOrDefaultAsync(f => f.CantonId == id);
            canton.Province.Cantons = null;

            if (canton == null)
            {
                return NotFound();
            }

            return canton;
        }

        // PUT: api/Cantons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanton(int id, Canton canton)
        {
            if (id != canton.CantonId)
            {
                return BadRequest();
            }

            _context.Entry(canton).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CantonExists(id))
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

        // POST: api/Cantons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canton>> PostCanton(Canton canton)
        {
          if (_context.Cantons == null)
          {
              return Problem("Entity set 'VeterinarianDB.Cantons'  is null.");
          }
            _context.Cantons.Add(canton);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanton", new { id = canton.CantonId }, canton);
        }

        // DELETE: api/Cantons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanton(int id)
        {
            if (_context.Cantons == null)
            {
                return NotFound();
            }
            var canton = await _context.Cantons.FindAsync(id);
            if (canton == null)
            {
                return NotFound();
            }

            _context.Cantons.Remove(canton);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CantonExists(int id)
        {
            return (_context.Cantons?.Any(e => e.CantonId == id)).GetValueOrDefault();
        }
    }
}
