using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;


public class Recipe{
    [Key]
    [Required]
    public int RecipeId { get; set; }
    public string Title {get;set;}
    public DateTime CreationDate {get;set;}
    public string Indications {get;set;}

    #region Relations
    public int? InscriptionId { get; set; }
    [ForeignKey(nameof(InscriptionId))]
    public Inscription? Inscription { get; set; }

    public int AnimalId { get; set; }
    [ForeignKey(nameof(AnimalId))]
    public Animal Animal { get; set; }
    #endregion

}