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
    public class VeterinarianController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public VeterinarianController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/Veterinarian
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinarian>>> GetVeterinarians()
        {
            if (_context.veterinarians == null)
            {
                return NotFound();
            }
            return await _context.veterinarians.ToListAsync();
        }


        // GET: api/Veterinarian/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinarian>> GetVeterinarian(int id)
        {
            if (_context.veterinarians == null)
            {
                return NotFound();
            }
            var veterinarian = _context.veterinarians.Where((e) => e.VeterinarianId == id).FirstOrDefault();

            if (veterinarian == null)
            {
                return NotFound();
            }
            //clinic.HashPassword=String.Empty;
            return veterinarian;
        }

        // PUT: api/Veterinarian/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinarian(int id, Veterinarian veterinarian)
        {
            if (id != veterinarian.VeterinarianId)
            {
                return BadRequest();
            }

            _context.Entry(veterinarian).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeterinarianExists(id))
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

        // POST: api/Veterinarian
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clinic>> PostVeterinarian(Veterinarian veterinarian)
        {
            if (_context.veterinarians == null)
            {
                return Problem("Entity set 'VeterinarianDB.Clinics'  is null.");
            }
            _context.veterinarians.Add(veterinarian);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinic", new { id = veterinarian.VeterinarianId }, veterinarian);
        }

        // DELETE: api/Veterinarian/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinarian(int id)
        {
            if (_context.veterinarians == null)
            {
                return NotFound();
            }
            var veterinarian = await _context.veterinarians.FindAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }

            _context.veterinarians.Remove(veterinarian);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeterinarianExists(int id)
        {
            return (_context.veterinarians?.Any(e => e.VeterinarianId == id)).GetValueOrDefault();
        }

    }

}