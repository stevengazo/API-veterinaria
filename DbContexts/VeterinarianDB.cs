using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using API.Models;
namespace API.DBContexts;

public class VeterinarianDB : Microsoft.EntityFrameworkCore.DbContext
{
    #region  Properties
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Canton> Cantons { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Diagnostic> Diagnostics { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Sex> Sexes { get; set; }
    public DbSet<Surgery> Surgeries { get; set; }
    public DbSet<TypeAnimal> TypeAnimals { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Veterinarian> veterinarians { get; set; }
    #endregion

    #region  Methods
    public VeterinarianDB(DbContextOptions<VeterinarianDB> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Generate Data Base

        // Crear instancias de provincias y cantones reales para Costa Rica

        // Crear provincias
        var sanJose = new Province { ProvinceId = 1, Name = "San Jose" };
        var limon = new Province { ProvinceId = 2, Name = "Limon" };
        var puntarenas = new Province { ProvinceId = 3, Name = "Puntarenas" };
        var heredia = new Province { ProvinceId = 4, Name = "Heredia" };
        var alajuela = new Province { ProvinceId = 5, Name = "Alajuela" };
        var cartago = new Province { ProvinceId = 6, Name = "Cartago" };
        var guanacaste = new Province { ProvinceId = 7, Name = "Guanacaste" };

        // Crear cantones para San Jose
        var sjCanton1 = new Canton { CantonId = 1, Name = "San Jose Canton", ProvinceId = sanJose.ProvinceId };
        var sjCanton2 = new Canton { CantonId = 2, Name = "Escazu", ProvinceId = sanJose.ProvinceId };

        // Crear cantones para Limon
        var limonCanton1 = new Canton { CantonId = 3, Name = "Limon Canton", ProvinceId = limon.ProvinceId };
        var limonCanton2 = new Canton { CantonId = 4, Name = "Pococi", ProvinceId = limon.ProvinceId };

        // Crear cantones para Puntarenas
        var puntarenasCanton1 = new Canton { CantonId = 5, Name = "Puntarenas Canton", ProvinceId = puntarenas.ProvinceId };
        var puntarenasCanton2 = new Canton { CantonId = 6, Name = "Esparza", ProvinceId = puntarenas.ProvinceId };

        // Crear cantones para Heredia
        var herediaCanton1 = new Canton { CantonId = 7, Name = "Heredia Canton", ProvinceId = heredia.ProvinceId };
        var herediaCanton2 = new Canton { CantonId = 8, Name = "Barva", ProvinceId = heredia.ProvinceId };

        // Crear cantones para Alajuela
        var alajuelaCanton1 = new Canton { CantonId = 9, Name = "Alajuela Canton", ProvinceId = alajuela.ProvinceId };
        var alajuelaCanton2 = new Canton { CantonId = 10, Name = "Grecia", ProvinceId = alajuela.ProvinceId };

        // Crear cantones para Cartago
        var cartagoCanton1 = new Canton { CantonId = 11, Name = "Cartago Canton", ProvinceId = cartago.ProvinceId };
        var cartagoCanton2 = new Canton { CantonId = 12, Name = "Paraíso", ProvinceId = cartago.ProvinceId };

        // Crear cantones para Guanacaste
        var guanacasteCanton1 = new Canton { CantonId = 13, Name = "Liberia", ProvinceId = guanacaste.ProvinceId };
        var guanacasteCanton2 = new Canton { CantonId = 14, Name = "Santa Cruz", ProvinceId = guanacaste.ProvinceId };

        // Agregar provincias y cantones al modelo
        modelBuilder.Entity<Province>().HasData(
            sanJose,
            limon,
            puntarenas,
            heredia,
            alajuela,
            cartago,
            guanacaste
        );

        modelBuilder.Entity<Canton>().HasData(
            sjCanton1,
            sjCanton2,
            limonCanton1,
            limonCanton2,
            puntarenasCanton1,
            puntarenasCanton2,
            herediaCanton1,
            herediaCanton2,
            alajuelaCanton1,
            alajuelaCanton2,
            cartagoCanton1,
            cartagoCanton2,
            guanacasteCanton1,
            guanacasteCanton2
        );

        // Crear instancias de tipos de animales y algunos animales para cada tipo

        var tipoMamifero = new TypeAnimal { TypeAnimalId = 1, TypeName = "Mamífero" };
        var tipoAve = new TypeAnimal { TypeAnimalId = 2, TypeName = "Ave" };

        var leon = new TypeAnimal { TypeAnimalId = 3, TypeName = "León", };
        var elefante = new TypeAnimal { TypeAnimalId = 4, TypeName = "Elefante", };

        var aguila = new TypeAnimal { TypeAnimalId = 5, TypeName = "Águila" };
        var loro = new TypeAnimal { TypeAnimalId = 6, TypeName = "Loro " };


        // Agregar tipos de animales y animales al modelo
        modelBuilder.Entity<TypeAnimal>().HasData(
            tipoMamifero,
            tipoAve,
            leon,
            elefante,
            aguila,
            loro
        );

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    #endregion

}