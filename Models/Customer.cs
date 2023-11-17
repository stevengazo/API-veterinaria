using System.ComponentModel.DataAnnotations;

namespace API.Models;
public class Customer : Person
{
    [Key]
    public int CustomerId { get; set; }
    public string UserName { get; set; }
    public string HashPassword { get; set; }
    public ICollection<Animal> Animals { get; set; }

}