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

namespace API.Controllers{



    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
{
    private readonly VeterinarianDB _context;

    public LoginController(VeterinarianDB context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var login = await _context.Clinics
            .FirstOrDefaultAsync(c => c.UserName == model.UserName && c.HashPassword == model.HashPassword);

        if (login == null)
        {
            return Unauthorized();
        }

        // Aquí puedes generar un token de autenticación si lo deseas.

        return Ok(new { Message = "Login exitoso" });
    }
}

}