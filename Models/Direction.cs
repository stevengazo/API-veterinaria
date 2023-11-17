using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Direction
{
    [Key]
    public int DirectionId { get; set; }
    public string DirectionDescription { get; set; }
    public Direction direction { get; set; }
    public int DistrictId { get; set; }
}