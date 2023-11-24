using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;
public class Customer : Person
{ 
    public string UserName { get; set; }
    public string HashPassword { get; set; }
    #region Relations
    public ICollection<Animal> Animals { get; set; }


    public Sex? Sex { get; set; } 
    public int SexId { get; set; }
    #endregion

}