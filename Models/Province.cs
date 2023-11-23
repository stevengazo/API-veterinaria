namespace API.Models;

public class Province
{
    public int ProvinceId { get; set; }
    public string Name { get; set; }
    #region Relations
    public ICollection<Canton> Cantons { get; set; }
    #endregion 
}