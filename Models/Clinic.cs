using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class Clinic 
{
    [Key]
    [Required]
    public int ClinicId { get; set; }
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    public string URLLogo { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string HashPassword { get; set; }
    public bool IsActive { get; set; }
    #region Relations
    public ICollection<Direction>? Directions { get; set; }
    public ICollection<Inscription>? inscriptions { get; set; }
    #endregion
}