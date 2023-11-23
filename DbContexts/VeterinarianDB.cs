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