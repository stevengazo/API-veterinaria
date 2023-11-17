using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Clinic
{
    [Key]
    public int ClinicId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber{get;set;}
    public string URLLogo {get;set;}
    public string Email {get;set;}
    public string UserName {get;set;}
    public string HashPassword {get;set;}
    public ICollection<Inscription> inscriptions { get; set; }

}