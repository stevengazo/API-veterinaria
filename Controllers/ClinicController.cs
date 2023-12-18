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
    public class ClinicController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public ClinicController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/Clinic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
        {
            if (_context.Clinics == null)
            {
                return NotFound();
            }
            return await _context.Clinics.ToListAsync();
        }

        // GET: api/Clinic/5
        [HttpGet("{UserName}")]
        public async Task<ActionResult<Clinic>> GetClinic(String UserName)
        {
            if (_context.Clinics == null)
            {
                return NotFound();
            }
            var clinic = _context.Clinics.Where((e) => e.UserName == UserName).FirstOrDefault();

            if (clinic == null)
            {
                return NotFound();
            }
            //clinic.HashPassword=String.Empty;
            return clinic;
        }


        // PUT: api/Clinic/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinic(int id, Clinic clinic)
        {
            if (id != clinic.ClinicId)
            {
                return BadRequest();
            }

            _context.Entry(clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
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

        // PUT: api/Veterinarian/5 Este put modifica todos los campos excepto la contrasenña
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditProfile/{id}")]
        public async Task<IActionResult> PutClinicWithout(int id, Clinic clinic)
        {
            if (id != clinic.ClinicId)
            {
                return BadRequest();
                //return NotFound();
            }

            // Evitar que se modifique el campo hashPassword
            var existingClinic = await _context.Clinics.FindAsync(id);

            if (existingClinic == null)
            {
                return NotFound();
            }

            // Copiar los valores permitidos desde el objeto enviado al objeto existente
            //existingClinic.ClinicId = clinic.ClinicId;
            existingClinic.Name = clinic.Name;
            existingClinic.PhoneNumber = clinic.PhoneNumber;
            existingClinic.URLLogo = clinic.URLLogo;
            existingClinic.Email = clinic.Email;
            existingClinic.UserName = clinic.UserName;
            existingClinic.Directions = clinic.Directions;
            existingClinic.inscriptions = clinic.inscriptions;
            // Otros campos que deseas permitir

            _context.Clinics.Update(existingClinic);

            //  _context.Entry(existingClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
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

        // PUT: api/Veterinarian/5 Este put modifica solamente la contraseña
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditProfilePassword/{id}")]
        public async Task<IActionResult> PutClinicPassword(int id, Clinic clinic)
        {
            if (id != clinic.ClinicId)
            {
                return BadRequest();
                //return NotFound();
            }

            // Evitar que se modifique el campo hashPassword
            var existingClinic = await _context.Clinics.FindAsync(id);

            if (existingClinic == null)
            {
                return NotFound();
            }

            // Copiar los valores permitidos desde el objeto enviado al objeto existente
            //existingClinic.ClinicId = clinic.ClinicId;
            existingClinic.HashPassword = clinic.HashPassword;

            _context.Clinics.Update(existingClinic);

            //  _context.Entry(existingClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
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

        // POST: api/Clinic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clinic>> PostClinic(Clinic clinic)
        {
            if (_context.Clinics == null)
            {
                return Problem("Entity set 'VeterinarianDB.Clinics'  is null.");
            }
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

        }

        // DELETE: api/Clinic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            if (_context.Clinics == null)
            {
                return NotFound();
            }
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClinicExists(int id)
        {
            return (_context.Clinics?.Any(e => e.ClinicId == id)).GetValueOrDefault();
        }
    }
}
