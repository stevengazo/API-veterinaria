using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Canton
{
    [Key]
    [Required]
    public int CantonId { get; set; }
    public string Name { get; set; }

    #region Relations
    public ICollection<District> Districts { get; set; }

    [ForeignKey(nameof(ProvinceId))]
    public Province? Province { get; set; }
    public int ProvinceId { get; set; }
    #endregion
}