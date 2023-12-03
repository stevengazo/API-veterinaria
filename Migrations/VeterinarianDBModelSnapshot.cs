﻿// <auto-generated />
using System;
using API.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(VeterinarianDB))]
    partial class VeterinarianDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeAnimalId")
                        .HasColumnType("int");

                    b.Property<string>("URLImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimalId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TypeAnimalId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("API.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateToMeet")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InscriptionId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("InscriptionId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("API.Models.Canton", b =>
                {
                    b.Property<int>("CantonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CantonId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("CantonId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cantons");
                });

            modelBuilder.Entity("API.Models.Clinic", b =>
                {
                    b.Property<int>("ClinicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClinicId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("URLLogo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClinicId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("API.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("SecondLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SexId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("SexId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("API.Models.Diagnostic", b =>
                {
                    b.Property<int>("DiagnosticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiagnosticId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int?>("InscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiagnosticId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("InscriptionId");

                    b.ToTable("Diagnostics");
                });

            modelBuilder.Entity("API.Models.Direction", b =>
                {
                    b.Property<int>("DirectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectionId"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DirectionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("DirectionId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("API.Models.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<int>("CantonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.HasIndex("CantonId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("API.Models.Inscription", b =>
                {
                    b.Property<int>("InscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InscriptionId"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<int>("VeterinarianId")
                        .HasColumnType("int");

                    b.HasKey("InscriptionId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("VeterinarianId");

                    b.ToTable("Inscriptions");
                });

            modelBuilder.Entity("API.Models.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProvinceId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProvinceId");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("API.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Indications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("InscriptionId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("API.Models.Sex", b =>
                {
                    b.Property<int>("SexId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SexId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SexId");

                    b.ToTable("Sexes");
                });

            modelBuilder.Entity("API.Models.Surgery", b =>
                {
                    b.Property<int>("SurgeryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SurgeryId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Creation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SurgeryId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("InscriptionId");

                    b.ToTable("Surgeries");
                });

            modelBuilder.Entity("API.Models.TypeAnimal", b =>
                {
                    b.Property<int>("TypeAnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeAnimalId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeAnimalId");

                    b.ToTable("TypeAnimals");
                });

            modelBuilder.Entity("API.Models.Vaccine", b =>
                {
                    b.Property<int>("VaccineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("AplicationDate")
                        .HasColumnType("int");

                    b.Property<int?>("InscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VaccineId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("InscriptionId");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("API.Models.Veterinarian", b =>
                {
                    b.Property<int>("VeterinarianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VeterinarianId"));

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("SecondLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SexId")
                        .HasColumnType("int");

                    b.HasKey("VeterinarianId");

                    b.HasIndex("SexId");

                    b.ToTable("veterinarians");
                });

            modelBuilder.Entity("API.Models.Animal", b =>
                {
                    b.HasOne("API.Models.Customer", "customer")
                        .WithMany("Animals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.TypeAnimal", "TypeAnimal")
                        .WithMany("Animals")
                        .HasForeignKey("TypeAnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeAnimal");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("API.Models.Appointment", b =>
                {
                    b.HasOne("API.Models.Animal", "Animal")
                        .WithMany("Appointments")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Inscription", "Inscription")
                        .WithMany("Appointments")
                        .HasForeignKey("InscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Animal");

                    b.Navigation("Inscription");
                });

            modelBuilder.Entity("API.Models.Canton", b =>
                {
                    b.HasOne("API.Models.Province", "Province")
                        .WithMany("Cantons")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("API.Models.Customer", b =>
                {
                    b.HasOne("API.Models.Sex", "Sex")
                        .WithMany("Customers")
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sex");
                });

            modelBuilder.Entity("API.Models.Diagnostic", b =>
                {
                    b.HasOne("API.Models.Animal", "Animal")
                        .WithMany("Diagnostics")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Inscription", "Inscription")
                        .WithMany("Diagnostics")
                        .HasForeignKey("InscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Animal");

                    b.Navigation("Inscription");
                });

            modelBuilder.Entity("API.Models.Direction", b =>
                {
                    b.HasOne("API.Models.Clinic", "Clinic")
                        .WithMany("Directions")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("API.Models.District", "District")
                        .WithMany("Directions")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Customer");

                    b.Navigation("District");
                });

            modelBuilder.Entity("API.Models.District", b =>
                {
                    b.HasOne("API.Models.Canton", "Canton")
                        .WithMany("Districts")
                        .HasForeignKey("CantonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canton");
                });

            modelBuilder.Entity("API.Models.Inscription", b =>
                {
                    b.HasOne("API.Models.Clinic", "Clinic")
                        .WithMany("inscriptions")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Veterinarian", "Veterinarian")
                        .WithMany("Inscriptions")
                        .HasForeignKey("VeterinarianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Veterinarian");
                });

            modelBuilder.Entity("API.Models.Recipe", b =>
                {
                    b.HasOne("API.Models.Animal", "Animal")
                        .WithMany("Recipes")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Inscription", "Inscription")
                        .WithMany("Recipes")
                        .HasForeignKey("InscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Animal");

                    b.Navigation("Inscription");
                });

            modelBuilder.Entity("API.Models.Surgery", b =>
                {
                    b.HasOne("API.Models.Animal", "Animal")
                        .WithMany("Surgeries")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Inscription", "Inscription")
                        .WithMany("Surgeries")
                        .HasForeignKey("InscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Animal");

                    b.Navigation("Inscription");
                });

            modelBuilder.Entity("API.Models.Vaccine", b =>
                {
                    b.HasOne("API.Models.Animal", "Animal")
                        .WithMany("Vaccines")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Inscription", "Inscription")
                        .WithMany("Vaccines")
                        .HasForeignKey("InscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Animal");

                    b.Navigation("Inscription");
                });

            modelBuilder.Entity("API.Models.Veterinarian", b =>
                {
                    b.HasOne("API.Models.Sex", "Sex")
                        .WithMany("Veterinarians")
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sex");
                });

            modelBuilder.Entity("API.Models.Animal", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Diagnostics");

                    b.Navigation("Recipes");

                    b.Navigation("Surgeries");

                    b.Navigation("Vaccines");
                });

            modelBuilder.Entity("API.Models.Canton", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("API.Models.Clinic", b =>
                {
                    b.Navigation("Directions");

                    b.Navigation("inscriptions");
                });

            modelBuilder.Entity("API.Models.Customer", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("API.Models.District", b =>
                {
                    b.Navigation("Directions");
                });

            modelBuilder.Entity("API.Models.Inscription", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Diagnostics");

                    b.Navigation("Recipes");

                    b.Navigation("Surgeries");

                    b.Navigation("Vaccines");
                });

            modelBuilder.Entity("API.Models.Province", b =>
                {
                    b.Navigation("Cantons");
                });

            modelBuilder.Entity("API.Models.Sex", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Veterinarians");
                });

            modelBuilder.Entity("API.Models.TypeAnimal", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("API.Models.Veterinarian", b =>
                {
                    b.Navigation("Inscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
