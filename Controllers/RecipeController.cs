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
    public class RecipeController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public RecipeController(VeterinarianDB context)
        {
            _context = context;
        }

        [HttpGet("GetByAnimal/{id}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetByAnimal(int id){
            return await _context.Recipes
                                .Include(D => D.Inscription.Veterinarian)
                                .Include(D => D.Inscription.Clinic)
                                .Where(D => D.AnimalId == id)
                                .ToListAsync();
        }

        [HttpGet("GetByVeterinarian/{id}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetByVeterinarian(int id)
        {
            return await _context.Recipes
                        .Include(D => D.Inscription.Veterinarian)
                        .Include(D => D.Inscription.Clinic)
                        .Where(D => D.InscriptionId == id)
                        .Include(A => A.Animal)
                        .ThenInclude(A => A.customer)
                        .OrderByDescending(a => a.CreationDate)
                        .ToListAsync();
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
          if (_context.Recipes == null)
          {
              return Problem("Entity set 'VeterinarianDB.Recipes'  is null.");
          }
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            var newRecipe = await _context.Recipes  
            .Include(D=>D.Inscription.Veterinarian)
            .Include(D=>D.Inscription.Clinic)
            .FirstOrDefaultAsync(x=>x.RecipeId == recipe.RecipeId);

            return CreatedAtAction("GetRecipe", new { id = newRecipe.RecipeId }, newRecipe);
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
