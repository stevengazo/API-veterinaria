namespace Models;

public class Direction
{
    public int DirectionId { get; set; }
    public string DirectionDescription { get; set; }
    public Direction direction { get; set; }
    public int DistrictId { get; set; }
}