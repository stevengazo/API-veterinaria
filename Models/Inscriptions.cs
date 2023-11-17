using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Inscription
{
    [Key]
    public int InscriptionId { get; set; }
    public Veterinarian Veterinarian { get; set; }
    public int VeterinarianId { get; set; }
    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Diagnostic> Diagnostics { get; set; }
    public ICollection<Recipe> Recipes {get;set;}
    public ICollection<Surgery> Surgeries {get;set;}
    public ICollection<Vaccine> Vaccines {get;set;}

}