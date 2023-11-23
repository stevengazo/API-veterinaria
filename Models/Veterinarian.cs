using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Veterinarian : Person
{
    public ICollection<Inscription> Inscriptions { get; set; }

    public int SexId { get; set; }
    [ForeignKey(nameof(SexId))]  
    public Sex Sex { get; set; }

}