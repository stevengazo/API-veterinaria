using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Surgery
{
    [Key]
    [Required]
    public int SurgeryId { get; set; }
    public string Title { get; set; }
    public DateTime Creation { get; set; }
    public string Description { get; set; }

    #region Relations
    public Inscription? Inscription { get; set; }
    public int? InscriptionId { get; set; }

    public Animal? Animal { get; set; }
    public int AnimalId { get; set; }
    #endregion
}