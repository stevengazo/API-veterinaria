using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  API.Models;

public class Vaccine{
    [Key]
    [Required]
    public int VaccineId { get; set; }
    public string Title{get;set;}
    public int AplicationDate {get;set;}

    #region Relations
    public Inscription? Inscription { get; set; }
    public int? InscriptionId { get; set; }
    public Animal? Animal { get; set; }
    public int AnimalId { get; set; }
    #endregion
}