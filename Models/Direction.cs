using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Direction
{
    [Key]
    [Required]
    public int DirectionId { get; set; }
    public string DirectionDescription { get; set; }
    #region Relations
    [ForeignKey(nameof(DistrictId))]
    public District District { get; set; }
    public int DistrictId { get; set; }

    public int PersonId { get; set; }
    [ForeignKey(nameof(PersonId))]
    public Customer Customer { get; set; }

    public int ClinicId { get; set; }
    [ForeignKey(nameof(ClinicId))]
    public Clinic Clinic { get; set; } 
    #endregion
}