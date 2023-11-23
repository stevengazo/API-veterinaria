using System.ComponentModel.DataAnnotations;

namespace  API.Models;

public class TypeAnimal
{
    [Key]
    [Required]
    public int TypeAnimalId { get; set; }
    public string TypeName {get;set;}

    #region Relations
    public ICollection<Animal> Animals {get;set;}
    #endregion

}