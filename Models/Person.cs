namespace Models;

public abstract class Person
{
    public int PersonId { get; set; }
    public int DNI { get; set; }
    public string IdentificationType { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string SecondLastName { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public Sex Sex { get; set; }
    public int SexId { get; set; }
}