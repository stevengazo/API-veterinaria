using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using API.DBContexts;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Azure.Storage.Blobs.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly VeterinarianDB _context;
        private readonly BlobServiceClient _blobServiceClient;

        public AnimalsController(VeterinarianDB context, BlobServiceClient blobService)
        {
            _context = context;
            _blobServiceClient = blobService;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
          if (_context.Animals == null)
          {
              return NotFound();
          }
            return await _context.Animals.ToListAsync();
        }
        // GET: api/Animals
        [HttpGet("ejemplo")]
        public async Task<ActionResult<IEnumerable<BlobContainerItem>>> ejemplo()
        {
            var sampleContainer = _blobServiceClient.GetBlobContainers().ToList();
            return sampleContainer.ToList();
        }

        

        [HttpPost("UploadFile/{id}")]
        public async Task<ActionResult> UploadFile(IFormFile data, int id)
        {
            var Container = _blobServiceClient.GetBlobContainers().FirstOrDefault();
            if (Container != null)
            {
               using(var stream = data.OpenReadStream()) {
                    _blobServiceClient.GetBlobContainerClient(Container.Name).UploadBlob(data.FileName, stream, CancellationToken.None);
                }

            }

            return Ok();
        }



        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
          if (_context.Animals == null)
          {
              return NotFound();
          }
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
          if (_context.Animals == null)
          {
              return Problem("Entity set 'VeterinarianDB.Animals'  is null.");
          }
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.AnimalId }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            if (_context.Animals == null)
            {
                return NotFound();
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animals?.Any(e => e.AnimalId == id)).GetValueOrDefault();
        }
    }
}
