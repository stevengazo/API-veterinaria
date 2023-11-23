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
    public class TypeAnimalsController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public TypeAnimalsController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/TypeAnimals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeAnimal>>> GetTypeAnimals()
        {
          if (_context.TypeAnimals == null)
          {
              return NotFound();
          }
            return await _context.TypeAnimals.ToListAsync();
        }

        // GET: api/TypeAnimals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeAnimal>> GetTypeAnimal(int id)
        {
          if (_context.TypeAnimals == null)
          {
              return NotFound();
          }
            var typeAnimal = await _context.TypeAnimals.FindAsync(id);

            if (typeAnimal == null)
            {
                return NotFound();
            }

            return typeAnimal;
        }

        // PUT: api/TypeAnimals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeAnimal(int id, TypeAnimal typeAnimal)
        {
            if (id != typeAnimal.TypeAnimalId)
            {
                return BadRequest();
            }

            _context.Entry(typeAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeAnimalExists(id))
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

        // POST: api/TypeAnimals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeAnimal>> PostTypeAnimal(TypeAnimal typeAnimal)
        {
          if (_context.TypeAnimals == null)
          {
              return Problem("Entity set 'VeterinarianDB.TypeAnimals'  is null.");
          }
            _context.TypeAnimals.Add(typeAnimal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeAnimal", new { id = typeAnimal.TypeAnimalId }, typeAnimal);
        }

        // DELETE: api/TypeAnimals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeAnimal(int id)
        {
            if (_context.TypeAnimals == null)
            {
                return NotFound();
            }
            var typeAnimal = await _context.TypeAnimals.FindAsync(id);
            if (typeAnimal == null)
            {
                return NotFound();
            }

            _context.TypeAnimals.Remove(typeAnimal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeAnimalExists(int id)
        {
            return (_context.TypeAnimals?.Any(e => e.TypeAnimalId == id)).GetValueOrDefault();
        }
    }
}
