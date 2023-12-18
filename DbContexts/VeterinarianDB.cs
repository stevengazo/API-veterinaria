using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Sex> Sexes { get; set; }
    public DbSet<Surgery> Surgeries { get; set; }
    public DbSet<TypeAnimal> TypeAnimals { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Veterinarian> veterinarians { get; set; }
    #endregion

    internal IConfiguration configuration { get; set; }


    /// <summary>
    /// Create a new configuration builder and connect to appsettings.json and read the 
    /// connection string and set the value
    /// </summary>
    private string GetConnectionString(string connectionStringName = "Veterinarian")
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .AddEnvironmentVariables();
        configuration = builder.Build();
        return configuration.GetConnectionString(connectionStringName);
    }

    #region  Methods
    public VeterinarianDB(DbContextOptions<VeterinarianDB> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        List<Sex> sexes = new(){
            new Sex(){
                SexId=1,
                Name="Hombre"
            },new Sex(){
                SexId=2,
                Name="Mujer"
            },
            new Sex(){
                SexId=3,
                Name="Otro"
            }
        };

        modelBuilder.Entity<Sex>().HasData(sexes);

        List<Province> provincias = new List<Province>
        {
            new Province { ProvinceId = 1, Name = "San José" },
            new Province { ProvinceId = 2, Name = "Alajuela" },
            new Province { ProvinceId = 3, Name = "Cartago" },
            new Province { ProvinceId = 4, Name = "Heredia" },
            new Province { ProvinceId = 5, Name = "Puntarenas" },
            new Province { ProvinceId = 6, Name = "Limón" },
            new Province { ProvinceId = 7, Name = "Guanacaste" }
        };
        modelBuilder.Entity<Province>().HasData(provincias);
       
        List<Canton> cantons = new List<Canton>
        {
            new Canton { CantonId = 1, Name = "San José", ProvinceId = 1 },
            new Canton { CantonId = 2, Name = "San Pedro", ProvinceId = 1 },
            new Canton { CantonId = 3, Name = "Alajuela", ProvinceId = 2 },
            new Canton { CantonId = 4, Name = "Grecia", ProvinceId = 2 },
            new Canton { CantonId = 5, Name = "Cartago", ProvinceId = 3 },
            new Canton { CantonId = 6, Name = "Paraíso", ProvinceId = 3 },
            new Canton { CantonId = 7, Name = "Santo Domingo", ProvinceId = 4 },
            new Canton { CantonId = 8, Name = "San Pablo", ProvinceId = 4 },
            new Canton { CantonId = 9, Name = "Puntarenas", ProvinceId = 5 },
            new Canton { CantonId = 10, Name = "Quepos", ProvinceId = 5 },
            new Canton { CantonId = 11, Name = "Limón", ProvinceId = 6 },
            new Canton { CantonId = 12, Name = "Guapiles", ProvinceId = 6 },
            new Canton { CantonId = 13, Name = "Guanacaste", ProvinceId = 7 },
            new Canton { CantonId = 14, Name = "Nicoya", ProvinceId = 7 }
        };
        modelBuilder.Entity<Canton>().HasData(cantons);

        List<District> districts = new List<District>
        {
            // San Jose
            new District { DistrictId = 1, Name = "San Miguel", CantonId = 1 },
            new District { DistrictId = 2, Name = "Escazú", CantonId = 1 },

            // San Pedro
            new District { DistrictId = 3, Name = "San Pedro", CantonId = 2 },
            new District { DistrictId = 4, Name = "San Rafael", CantonId = 2 },

            // Alajuela
            new District { DistrictId = 5, Name = "Alajuela", CantonId = 3 },
            new District { DistrictId = 6, Name = "San Ramón", CantonId = 3 },

            // Grecia
            new District { DistrictId = 7, Name = "Grecia", CantonId = 4 },
            new District { DistrictId = 8, Name = "Sarchí", CantonId = 4 },

            // Cartago
            new District { DistrictId = 9, Name = "Cartago", CantonId = 5 },
            new District { DistrictId = 10, Name = "Paraíso", CantonId = 5 },

            // Paraíso
            new District { DistrictId = 11, Name = "Santo Domingo", CantonId = 6 },
            new District { DistrictId = 12, Name = "San Vicente", CantonId = 6 },

            // Santo Domingo
            new District { DistrictId = 13, Name = "Santo Domingo", CantonId = 7 },
            new District { DistrictId = 14, Name = "San Juanillo", CantonId = 7 },

            // San Pablo
            new District { DistrictId = 15, Name = "San Pablo", CantonId = 8 },
            new District { DistrictId = 16, Name = "San Isidro", CantonId = 8 },

            // Puntarenas
            new District { DistrictId = 17, Name = "Puntarenas", CantonId = 9 },
            new District { DistrictId = 18, Name = "Chacarita", CantonId = 9 },

            // Quepos
            new District { DistrictId = 19, Name = "Quepos", CantonId = 10 },
            new District { DistrictId = 20, Name = "Parrita", CantonId = 10 },

            // Limón
            new District { DistrictId = 21, Name = "Limón", CantonId = 11 },
            new District { DistrictId = 22, Name = "Guácimo", CantonId = 11 },

            // Guápiles
            new District { DistrictId = 23, Name = "Guápiles", CantonId = 12 },
            new District { DistrictId = 24, Name = "Siquirres", CantonId = 12 },

            // Guanacaste
            new District { DistrictId = 25, Name = "Liberia", CantonId = 13 },
            new District { DistrictId = 26, Name = "Santa Cruz", CantonId = 13 },

            // Nicoya
            new District { DistrictId = 27, Name = "Nicoya", CantonId = 14 },
            new District { DistrictId = 28, Name = "Santa Cruz", CantonId = 14 }
        };

        modelBuilder.Entity<District>().HasData(districts);

        List<TypeAnimal> typeAnimals= new(){
            new TypeAnimal(){
                TypeAnimalId=1,
                TypeName="Perro"
            },
            new TypeAnimal(){
                TypeAnimalId=2,
                TypeName="Gato"
            },
            new TypeAnimal(){
                TypeAnimalId=3,
                TypeName="Conejo"
            },
            new TypeAnimal(){
                TypeAnimalId=4,
                TypeName="Ave"
            },
            new TypeAnimal(){
                TypeAnimalId=5,
                TypeName="Cabra"
            },
        };

        modelBuilder.Entity<TypeAnimal>().HasData(typeAnimals);

        Customer customer =new(){
            CustomerId= 1,
            UserName= "default",
            HashPassword= "default",
            DNI = 11111,
            IdentificationType="National",
            Name= "nombre",
            LastName="Prueba",
            SecondLastName= "prueba",
            PhoneNumber= 888888,
            Email = "sample@mail.com",
            SexId = 1
        };
        modelBuilder.Entity<Customer>().HasData(customer);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

    }
    #endregion

}