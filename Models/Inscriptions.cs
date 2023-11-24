using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Inscription
{
    [Key]
    [Required]
    public int InscriptionId { get; set; }

    #region Relations


    public Veterinarian? Veterinarian { get; set; }
    public int PersonId { get; set; }

    public int ClinicId { get; set; }

    public Clinic? Clinic { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Appointment>? Appointments { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Diagnostic>? Diagnostics { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Recipe>? Recipes {get;set;}
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Surgery>? Surgeries {get;set;}
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Vaccine>? Vaccines {get;set;}

    #endregion

}