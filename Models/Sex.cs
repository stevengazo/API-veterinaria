namespace Models;

public class Sex
{
    public int SexId { get; set; }
    public string Name { get; set; }
    public ICollection<Person> People { get; set; }
}