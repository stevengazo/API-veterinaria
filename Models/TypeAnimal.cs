namespace  API.Models;

public class TypeAnimal
{
    public int TypeAnimalId { get; set; }
    public string TypeName {get;set;}

    public ICollection<Animal> Animals {get;set;}
    
}