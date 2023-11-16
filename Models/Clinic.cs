namespace Models;

public class Clinic
{
    public int ClinicId { get; set; }
    public string Name { get; set; }
    public ICollection<Inscription> inscriptions { get; set; }

}