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
    public class DiagnosticController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public DiagnosticController(VeterinarianDB context)
        {
            _context = context;
        }

        [HttpGet("GetDiagnosticByAnimal/{id}")]
        public async Task<ActionResult<IEnumerable<Diagnostic>>> GetDiagnosticByAnimal(int id)
        {
            return await _context.Diagnostics
            .Include(D=>D.Inscription.Veterinarian)
            .Include(D=>D.Inscription.Clinic)
            .Where(D => D.AnimalId == id).ToListAsync();
        }

        // GET: api/Diagnostic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnostic>>> GetDiagnostics()
        {
            if (_context.Diagnostics == null)
            {
                return NotFound();
            }
            return await _context.Diagnostics.ToListAsync();
        }

        // GET: api/Diagnostic/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnostic>> GetDiagnostic(int id)
        {
            if (_context.Diagnostics == null)
            {
                return NotFound();
            }
            var diagnostic = await _context.Diagnostics.FindAsync(id);

            if (diagnostic == null)
            {
                return NotFound();
            }

            return diagnostic;
        }

        // PUT: api/Diagnostic/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostic(int id, Diagnostic diagnostic)
        {
            if (id != diagnostic.DiagnosticId)
            {
                return BadRequest();
            }

            _context.Entry(diagnostic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosticExists(id))
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

        // POST: api/Diagnostic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnostic>> PostDiagnostic(Diagnostic diagnostic)
        {
            if (_context.Diagnostics == null)
            {
                return Problem("Entity set 'VeterinarianDB.Diagnostics'  is null.");
            }
            _context.Diagnostics.Add(diagnostic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnostic", new { id = diagnostic.DiagnosticId }, diagnostic);
        }

        // DELETE: api/Diagnostic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostic(int id)
        {
            if (_context.Diagnostics == null)
            {
                return NotFound();
            }
            var diagnostic = await _context.Diagnostics.FindAsync(id);
            if (diagnostic == null)
            {
                return NotFound();
            }

            _context.Diagnostics.Remove(diagnostic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiagnosticExists(int id)
        {
            return (_context.Diagnostics?.Any(e => e.DiagnosticId == id)).GetValueOrDefault();
        }
    }
}
