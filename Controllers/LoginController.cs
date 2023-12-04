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


public interface UserInterface
{
    Task<bool> AuthenticateAsync(string userName, string password);
}

public class AuthenticateService : UserInterface
{
    private readonly SignInManager<Clinic> _signInManager;

    public AuthenticateService(SignInManager<Clinic> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> AuthenticateAsync(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }
}

    [Route("api/[controller]")]
    [ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthenticateService _authService;

    public AuthController(AuthenticateService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }

        var isAuthenticated = await _authService.AuthenticateAsync(model.UserName, model.HashPassword);

        if (isAuthenticated)
        {
            // Usuario autenticado
            return Ok(new { Message = "Login successful" });
        }
        else
        {
            // Autenticación fallida
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }
}


}