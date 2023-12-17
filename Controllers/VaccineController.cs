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
    public class VaccineController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public VaccineController(VeterinarianDB context)
        {
            _context = context;
        }

        [HttpGet("GetByAnimal/{id}")]
        public async Task<ActionResult<IEnumerable<Vaccine>>> GetByAnimal(int id){
            return await _context.Vaccines
            .Include(D => D.Inscription.Veterinarian)
            .Include(D => D.Inscription.Clinic)
            .Where(D => D.AnimalId == id)
            .ToListAsync();
        }


        // GET: api/Vaccine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccine>>> GetVaccines()
        {
            if (_context.Vaccines == null)
            {
                return NotFound();
            }
            return await _context.Vaccines.ToListAsync();
        }

        // GET: api/Vaccine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccine>> GetVaccine(int id)
        {
            if (_context.Vaccines == null)
            {
                return NotFound();
            }
            var vaccine = await _context.Vaccines.FindAsync(id);

            if (vaccine == null)
            {
                return NotFound();
            }

            return vaccine;
        }

        // PUT: api/Vaccine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccine(int id, Vaccine vaccine)
        {
            if (id != vaccine.VaccineId)
            {
                return BadRequest();
            }

            _context.Entry(vaccine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineExists(id))
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

        // POST: api/Vaccine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vaccine>> PostVaccine(Vaccine vaccine)
        {
            if (_context.Vaccines == null)
            {
                return Problem("Entity set 'VeterinarianDB.Vaccines'  is null.");
            }
            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();
            vaccine = await _context.Vaccines
                        .Include(D=>D.Inscription.Veterinarian)
                        .Include(D=>D.Inscription.Clinic)
                        .FirstOrDefaultAsync(x=>x.VaccineId == vaccine.VaccineId);

            return CreatedAtAction("GetVaccine", new { id = vaccine.VaccineId }, vaccine);
        }

        // DELETE: api/Vaccine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            if (_context.Vaccines == null)
            {
                return NotFound();
            }
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            _context.Vaccines.Remove(vaccine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VaccineExists(int id)
        {
            return (_context.Vaccines?.Any(e => e.VaccineId == id)).GetValueOrDefault();
        }
    }
}
