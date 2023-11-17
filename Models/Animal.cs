using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Animal
{
    [Key]
    public int AnimalId { get; set; }
    public string? Name { get; set; }
    public string URLImage {get;set;}
    public bool IsActive { get; set; }
    public Customer customer { get; set; }
    public int CustomerId { get; set; }
    public int TypeAnimalId { get; set; }
    public TypeAnimal TypeAnimal {get;set;}
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Diagnostic> Diagnostics { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
    public ICollection<Surgery> Surgeries { get; set; }
    public ICollection<Vaccine> Vaccines { get; set; }
}