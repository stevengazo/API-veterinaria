namespace Models;

public class Veterinarian : Person
{
    public int VeterinarianId { get; set; }
    public ICollection<Inscription> Inscriptions { get; set; }

}