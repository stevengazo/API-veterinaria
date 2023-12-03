using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;
public class Customer : Person
{
    [Key]
    [Required]
    public int CustomerId { get; set; }
    public string UserName { get; set; }
    public string HashPassword { get; set; }
    #region Relations
    public ICollection<Animal> Animals { get; set; }


    #endregion

}