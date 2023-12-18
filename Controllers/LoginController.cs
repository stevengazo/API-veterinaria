using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using API.DBContexts;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly VeterinarianDB _context;

        public LoginController(VeterinarianDB context)
        {
            _context = context;
        }

        [HttpPost("Clinic")]
        public async Task<IActionResult> Clinic([FromBody] Login model)
        {
            var login = await _context.Clinics
                .FirstOrDefaultAsync(c => c.UserName == model.UserName && c.HashPassword == model.HashPassword);

            if (login == null)
            {
                return Unauthorized();
            }

            return Ok(new { Message = "Login exitoso" });
        }


        [HttpPost("Customer")]
        public async Task<IActionResult> Customer([FromBody] Login model)
        {
            var login = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserName == model.UserName && c.HashPassword == model.HashPassword);

            if (login == null)
            {
                return Unauthorized();
            }
            else
            {
                login.HashPassword = string.Empty;
                return Ok(login);
            }

        }
    }

}