using API.DBContexts;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionStringVeterinarian = builder.Configuration.GetConnectionString("Veterinarian");
string ConnectionStringBlobStorage = builder.Configuration.GetConnectionString("BlobStorage");

// Dependency Injection of Database
builder.Services.AddDbContext<VeterinarianDB>(options => options.UseSqlServer(connectionStringVeterinarian));


// Dependency Injection Of blobStorage
builder.Services.AddSingleton( Data=> new BlobServiceClient(ConnectionStringBlobStorage));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Enable CORS
const string CorsName = "DefaultPolicy";

builder.Services.AddCors( options=>{ 
    options.DefaultPolicyName = CorsName;

    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();  
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
