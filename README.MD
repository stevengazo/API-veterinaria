# Sistema de Veterinarias

## Descripción

APi de gestión RESTful

## Requisitos

Asegúrate de tener instalados los siguientes requisitos antes de ejecutar el proyecto:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

## Ejecutar Localmente

### Usando .NET CLI


# Desde la raíz del proyecto
dotnet restore
dotnet build
dotnet run --project API/API.csproj



# Usando Docker 
docker run -p 8080:80 -e Veterinarian="valor_veterinarian" -e BlobStorage="valor_blobstorage" nombre_de_tu_imagen


