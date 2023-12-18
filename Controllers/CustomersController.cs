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
    public class CustomersController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public CustomersController(VeterinarianDB context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

// PUT: api/Veterinarian/5 Este put modifica solamente la contraseña
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditProfilePassword/{id}")]
        public async Task<IActionResult> PutClinicPassword(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
                //return NotFound();
            }
 
            // Evitar que se modifique el campo hashPassword
            var existingCustomer = await _context.Customers.FindAsync(id);
 
            if (existingCustomer == null)
            {
                return NotFound();
            }
 
            // Copiar los valores permitidos desde el objeto enviado al objeto existente
            //existingClinic.ClinicId = clinic.ClinicId;
            existingCustomer.HashPassword = customer.HashPassword;
 
            _context.Customers.Update(existingCustomer);
 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
 

        [HttpGet("{UserName}")]
        public async Task<ActionResult<Customer>> GetCustomerUsername(String UserName)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = _context.Customers.Where((e) => e.UserName == UserName).FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }
            //customer.HashPassword=String.Empty;
            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }



            try
            {
                var cust = _context.Customers.FirstOrDefault(i => i.CustomerId == id);
                customer.HashPassword = cust.HashPassword;

                _context.Customers.Update(customer);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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



        //Con esto editamos sin cambiar la contrase;a
        [HttpPut("EditProfile/{id}")]
        public async Task<IActionResult> PutCustomerWithout(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
                //return NotFound();
            }

            // Evitar que se modifique el campo hashPassword
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Copiar los valores permitidos desde el objeto enviado al objeto existente
            //existingClinic.ClinicId = clinic.ClinicId;
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Name = customer.Name;
            existingCustomer.DNI = customer.DNI;
            existingCustomer.Name = customer.Name;
            existingCustomer.LastName = customer.LastName;

            existingCustomer.SecondLastName = customer.SecondLastName;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.SexId = customer.SexId;
            // Otros campos que deseas permitir

            _context.Customers.Update(existingCustomer);

            //  _context.Entry(existingClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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









        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'VeterinarianDB.Customers'  is null.");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
