using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Veterinarian : Person
{
    [Key]
    [Required]
    public int VeterinarianId { get; set; }
    public ICollection<Inscription> Inscriptions { get; set; }


}

