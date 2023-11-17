namespace API.Models;

public class Province
{
    public int ProvinceId { get; set; }
    public string Name { get; set; }
    public ICollection<Canton> Cantons { get; set; }
}