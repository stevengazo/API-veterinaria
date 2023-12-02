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
using Azure.Storage;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        #region Properties
        private readonly VeterinarianDB _context;
        private readonly BlobServiceClient _blobServiceClient;
        #endregion

        #region Constructor
        public AnimalsController(VeterinarianDB context, BlobServiceClient blobService)
        {
            _context = context;
            _blobServiceClient = blobService;
        }
        #endregion
        #region  Public Methods
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
        public async Task<ActionResult<string>> UploadFile(IFormFile data, int id)
        {

            Animal AnimalSelected = _context.Animals.Where(a => a.AnimalId == id).FirstOrDefault();

            if (AnimalSelected !=null)
            {
                string containerName = "images";
                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                if (container != null)
                {
                    using (var stream = data.OpenReadStream())
                    {
                        var blobName = data.FileName;

                        // Sube el blob al contenedor
                        _blobServiceClient.GetBlobContainerClient(containerName).UploadBlob(blobName, stream, CancellationToken.None);

                        // Obtiene la SAS para el blob
                        var expiry = DateTime.UtcNow.AddHours(1); // Por ejemplo, la SAS será válida durante 1 hora
                        var sas = GetSasForBlob(blobName, containerName, expiry, BlobAccountSasPermissions.Read);

                        // Construye y devuelve la URL pública
                        AnimalSelected.URLImage = sas.AbsoluteUri;
                        _context.Animals.Update(AnimalSelected);
                        _context.SaveChanges();
                        return Ok(sas.AbsoluteUri);
                    }
                }
            }
            else
            {
                return NotFound("No se encontró ningun id");
            }
            return NotFound("No se encontró el contenedor.");
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
        #endregion
        private Uri GetSasForBlob(string blobname, string containerName, DateTime expiry, BlobAccountSasPermissions permissions = BlobAccountSasPermissions.Read)
        {
            var offset = TimeSpan.FromMinutes(10);

            var credential = new StorageSharedKeyCredential("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==");
            var sas = new BlobSasBuilder
            {
                BlobName = blobname,
                BlobContainerName = containerName,
                StartsOn = DateTime.UtcNow.Subtract(offset),
                ExpiresOn = expiry.Add(offset)
            };
            sas.SetPermissions(permissions);

            UriBuilder sasUri = new UriBuilder($"http://127.0.0.1:10000/devstoreaccount1/{containerName}/{blobname}");
            sasUri.Query = sas.ToSasQueryParameters(credential).ToString();

            return sasUri.Uri;
        }

    }
}
