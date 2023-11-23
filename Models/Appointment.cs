using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace API.Models;


public class Appointment
{
    [Key]
    [Required]
    public int AppointmentId { get; set; }
    public DateTime DateToMeet { get; set; }

    #region Relations

    [ForeignKey(nameof(InscriptionId))]
    public Inscription? Inscription { get; set; }
    public int? InscriptionId { get; set; }

    [Required]
    [ForeignKey(nameof(AnimalId))]
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }

    #endregion
}