# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia el archivo del proyecto y restaura las dependencias
COPY ["API.csproj", "."]
RUN dotnet restore "./API.csproj"

# Copia el resto de los archivos y realiza la compilación
COPY . .
WORKDIR "/src/."
RUN dotnet build "API.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final
FROM base AS final
WORKDIR /app

# Copia los archivos publicados desde la etapa de publicación
COPY --from=publish /app/publish .

# Configura las variables de entorno para las cadenas de conexión
ENV VeterinarianConnectionString="tu_valor_de_conexion_para_Veterinarian"
ENV BlobStorageConnectionString="tu_valor_de_conexion_para_BlobStorage"

# Punto de entrada para ejecutar la aplicación al iniciar el contenedor
ENTRYPOINT ["dotnet", "API.dll"]
