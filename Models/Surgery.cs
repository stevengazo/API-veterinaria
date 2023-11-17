namespace API.Models;

public class Surgery
{
    public int SurgeryId { get; set; }
    public string Title { get; set; }
    public DateTime Creation { get; set; }
    public string Description { get; set; }
    public Inscription Inscription { get; set; }
    public int InscriptionId { get; set; }
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }
}