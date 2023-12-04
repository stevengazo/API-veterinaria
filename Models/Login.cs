using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Login
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string HashPassword { get; set; }
}