namespace Models;

public class District
{
    public int DistrictId { get; set; }
    public string Name { get; set; }
    public Canton Canton { get; set; }
    public int CantonId { get; set; }
    public ICollection<Direction> Directions { get; set; }
}