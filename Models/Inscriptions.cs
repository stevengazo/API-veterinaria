namespace Models;

public class Inscription
{
    public int InscriptionId { get; set; }
    public Veterinarian Veterinarian { get; set; }
    public int VeterinarianId { get; set; }
    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; }

}