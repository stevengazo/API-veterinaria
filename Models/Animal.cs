namespace Models;

public class Animal
{
    public int AnimalId { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public Customer customer { get; set; }
    public int CustomerId { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Diagnostic> Diagnostics { get; set; }
}