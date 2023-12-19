using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Sex
{
    [Key]
    [Required]
    public int SexId { get; set; }
    public string Name { get; set; }
    #region Relations
    public ICollection<Customer>? Customers { get; set; }
    public ICollection<Veterinarian>? Veterinarians { get; set; }
    #endregion
}