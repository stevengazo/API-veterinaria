using System.Diagnostics;
using System.Text.Json.Serialization;
using API.DBContexts;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionStringVeterinarian = builder.Configuration.GetConnectionString("Veterinarian");
string ConnectionStringBlobStorage = builder.Configuration.GetConnectionString("BlobStorage");

if (string.IsNullOrEmpty(connectionStringVeterinarian))
{
    throw new Exception("La cadena de conexión Veterinarian no ha sido especificada");
}


// Dependency Injection of Database
builder.Services.AddDbContext<VeterinarianDB>(options => options.UseSqlServer(connectionStringVeterinarian))
                .BuildServiceProvider();

// Dependency Injection Of blobStorage
builder.Services.AddSingleton(Data => new BlobServiceClient(ConnectionStringBlobStorage));

builder.Services.AddControllers()
                .AddJsonOptions(X => X.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
const string myOrigins = "_myallowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(  name: myOrigins,
                        policy =>
                        {
                            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        });
});

var app = builder.Build();
// Migration of Database
using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<VeterinarianDB>();
    if (dbcontext.Database.CanConnect())
    {
        Console.WriteLine("Ya existe la base de datos");
    }
    else
    {
        Console.WriteLine("La base de datos no existe. Intentando crearla");
        try
        {
            dbcontext.Database.Migrate();
            Console.WriteLine("Base de datos creada");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error al intentar crear la base de datos: {ex.Message}");
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplication of CORS
app.UseCors(myOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
