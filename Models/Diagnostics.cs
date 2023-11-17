using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Diagnostic
{
    [Key]
    public int DiagnosticId { get; set; }
    public string Title { get; set; }
    public Inscription Inscription { get; set; }
    public int InscriptionId { get; set; }
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }
}