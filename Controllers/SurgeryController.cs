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
    public class SurgeryController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public SurgeryController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/Surgery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Surgery>>> GetSurgeries()
        {
          if (_context.Surgeries == null)
          {
              return NotFound();
          }
            return await _context.Surgeries.ToListAsync();
        }

        // GET: api/Surgery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Surgery>> GetSurgery(int id)
        {
          if (_context.Surgeries == null)
          {
              return NotFound();
          }
            var surgery = await _context.Surgeries.FindAsync(id);

            if (surgery == null)
            {
                return NotFound();
            }

            return surgery;
        }

        // PUT: api/Surgery/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurgery(int id, Surgery surgery)
        {
            if (id != surgery.SurgeryId)
            {
                return BadRequest();
            }

            _context.Entry(surgery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurgeryExists(id))
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

        // POST: api/Surgery
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Surgery>> PostSurgery(Surgery surgery)
        {
          if (_context.Surgeries == null)
          {
              return Problem("Entity set 'VeterinarianDB.Surgeries'  is null.");
          }
            _context.Surgeries.Add(surgery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurgery", new { id = surgery.SurgeryId }, surgery);
        }

        // DELETE: api/Surgery/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurgery(int id)
        {
            if (_context.Surgeries == null)
            {
                return NotFound();
            }
            var surgery = await _context.Surgeries.FindAsync(id);
            if (surgery == null)
            {
                return NotFound();
            }

            _context.Surgeries.Remove(surgery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurgeryExists(int id)
        {
            return (_context.Surgeries?.Any(e => e.SurgeryId == id)).GetValueOrDefault();
        }
    }
}
