namespace  API.Models;

public class Vaccine{
    public int VaccineId { get; set; }
    public string Title{get;set;}
    public int AplicationDate {get;set;}
    public Inscription Inscription { get; set; }
    public int InscriptionId { get; set; }
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }
}