using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class Login : IdentityUser
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string HashPassword { get; set; }
}