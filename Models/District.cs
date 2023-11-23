using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class District
{
    [Key]
    [Required]
    public int DistrictId { get; set; }
    public string Name { get; set; }
    #region Relations
    [ForeignKey(nameof(CantonId))]
    public Canton Canton { get; set; }
    public int CantonId { get; set; }
    public ICollection<Direction> Directions { get; set; }
    #endregion
}