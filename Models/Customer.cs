namespace Models;
public class Customer : Person
{
    public int CustomerId { get; set; }
    public string UserName { get; set; }
    public string HashPassword { get; set; }
    public ICollection<Animal> Animals { get; set; }

}