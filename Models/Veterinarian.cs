using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Veterinarian : Person
{
    public ICollection<Inscription> Inscriptions { get; set; }

    public int SexId { get; set; }
    public Sex? Sex { get; set; }

}