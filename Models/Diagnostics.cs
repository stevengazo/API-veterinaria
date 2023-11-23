using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Diagnostic
{
    [Key]
    [Required]
    public int DiagnosticId { get; set; }
    public string Title { get; set; }

    #region Relations
    [ForeignKey(nameof(DiagnosticId))]
    public Inscription? Inscription { get; set; }
    public int? InscriptionId { get; set; }

    [ForeignKey(nameof(AnimalId))]
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }
    #endregion
}