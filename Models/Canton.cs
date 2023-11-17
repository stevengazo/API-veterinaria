using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Canton
{
    [Key]
    public int CantonId { get; set; }
    public string Name { get; set; }
    public ICollection<District> Districts { get; set; }
    public Province Province { get; set; }
    public int ProvinceId { get; set; }
}