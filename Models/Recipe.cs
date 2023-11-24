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

    public Inscription? Inscription { get; set; }

    public int AnimalId { get; set; }

    public Animal? Animal { get; set; }
    #endregion

}