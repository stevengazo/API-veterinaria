using System.ComponentModel.DataAnnotations;

namespace API.Models;


public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }
    public DateTime DateToMeet { get; set; }
    public Inscription Inscription { get; set; }
    public int InscriptionId { get; set; }
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }
}