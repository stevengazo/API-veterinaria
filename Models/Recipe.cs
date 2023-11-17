namespace API.Models;


public class Recipe{
    public int RecipeId { get; set; }
    public string Title {get;set;}
    public DateTime CreationDate {get;set;}
    public string Indications {get;set;}

    public Inscription Inscription { get; set; }
    public int InscriptionId { get; set; }
    public Animal Animal { get; set; }
    public int AnimalId { get; set; }

}