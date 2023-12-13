IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clinics] (
    [ClinicId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [PhoneNumber] int NOT NULL,
    [URLLogo] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [HashPassword] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Clinics] PRIMARY KEY ([ClinicId])
);
GO

CREATE TABLE [Provinces] (
    [ProvinceId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([ProvinceId])
);
GO

CREATE TABLE [Sexes] (
    [SexId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Sexes] PRIMARY KEY ([SexId])
);
GO

CREATE TABLE [TypeAnimals] (
    [TypeAnimalId] int NOT NULL IDENTITY,
    [TypeName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TypeAnimals] PRIMARY KEY ([TypeAnimalId])
);
GO

CREATE TABLE [Cantons] (
    [CantonId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ProvinceId] int NOT NULL,
    CONSTRAINT [PK_Cantons] PRIMARY KEY ([CantonId]),
    CONSTRAINT [FK_Cantons_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([ProvinceId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customers] (
    [CustomerId] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [DNI] int NOT NULL,
    [IdentificationType] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [SecondLastName] nvarchar(max) NOT NULL,
    [PhoneNumber] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [SexId] int NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId]),
    CONSTRAINT [FK_Customers_Sexes_SexId] FOREIGN KEY ([SexId]) REFERENCES [Sexes] ([SexId]) ON DELETE CASCADE
);
GO

CREATE TABLE [veterinarians] (
    [VeterinarianId] int NOT NULL IDENTITY,
    [DNI] int NOT NULL,
    [IdentificationType] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [SecondLastName] nvarchar(max) NOT NULL,
    [PhoneNumber] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [SexId] int NOT NULL,
    CONSTRAINT [PK_veterinarians] PRIMARY KEY ([VeterinarianId]),
    CONSTRAINT [FK_veterinarians_Sexes_SexId] FOREIGN KEY ([SexId]) REFERENCES [Sexes] ([SexId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Districts] (
    [DistrictId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CantonId] int NOT NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY ([DistrictId]),
    CONSTRAINT [FK_Districts_Cantons_CantonId] FOREIGN KEY ([CantonId]) REFERENCES [Cantons] ([CantonId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Animals] (
    [AnimalId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [URLImage] nvarchar(max) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [CustomerId] int NOT NULL,
    [TypeAnimalId] int NOT NULL,
    CONSTRAINT [PK_Animals] PRIMARY KEY ([AnimalId]),
    CONSTRAINT [FK_Animals_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Animals_TypeAnimals_TypeAnimalId] FOREIGN KEY ([TypeAnimalId]) REFERENCES [TypeAnimals] ([TypeAnimalId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inscriptions] (
    [InscriptionId] int NOT NULL IDENTITY,
    [VeterinarianId] int NOT NULL,
    [ClinicId] int NOT NULL,
    CONSTRAINT [PK_Inscriptions] PRIMARY KEY ([InscriptionId]),
    CONSTRAINT [FK_Inscriptions_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([ClinicId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Inscriptions_veterinarians_VeterinarianId] FOREIGN KEY ([VeterinarianId]) REFERENCES [veterinarians] ([VeterinarianId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Directions] (
    [DirectionId] int NOT NULL IDENTITY,
    [DirectionDescription] nvarchar(max) NOT NULL,
    [DistrictId] int NOT NULL,
    [PersonId] int NOT NULL,
    [CustomerId] int NULL,
    [ClinicId] int NOT NULL,
    CONSTRAINT [PK_Directions] PRIMARY KEY ([DirectionId]),
    CONSTRAINT [FK_Directions_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([ClinicId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Directions_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]),
    CONSTRAINT [FK_Directions_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([DistrictId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Appointments] (
    [AppointmentId] int NOT NULL IDENTITY,
    [DateToMeet] datetime2 NOT NULL,
    [InscriptionId] int NULL,
    [AnimalId] int NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY ([AppointmentId]),
    CONSTRAINT [FK_Appointments_Animals_AnimalId] FOREIGN KEY ([AnimalId]) REFERENCES [Animals] ([AnimalId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Inscriptions_InscriptionId] FOREIGN KEY ([InscriptionId]) REFERENCES [Inscriptions] ([InscriptionId])
);
GO

CREATE TABLE [Diagnostics] (
    [DiagnosticId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [InscriptionId] int NULL,
    [AnimalId] int NOT NULL,
    CONSTRAINT [PK_Diagnostics] PRIMARY KEY ([DiagnosticId]),
    CONSTRAINT [FK_Diagnostics_Animals_AnimalId] FOREIGN KEY ([AnimalId]) REFERENCES [Animals] ([AnimalId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Diagnostics_Inscriptions_InscriptionId] FOREIGN KEY ([InscriptionId]) REFERENCES [Inscriptions] ([InscriptionId])
);
GO

CREATE TABLE [Recipes] (
    [RecipeId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [Indications] nvarchar(max) NOT NULL,
    [InscriptionId] int NULL,
    [AnimalId] int NOT NULL,
    CONSTRAINT [PK_Recipes] PRIMARY KEY ([RecipeId]),
    CONSTRAINT [FK_Recipes_Animals_AnimalId] FOREIGN KEY ([AnimalId]) REFERENCES [Animals] ([AnimalId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Recipes_Inscriptions_InscriptionId] FOREIGN KEY ([InscriptionId]) REFERENCES [Inscriptions] ([InscriptionId])
);
GO

CREATE TABLE [Surgeries] (
    [SurgeryId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Creation] datetime2 NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [InscriptionId] int NULL,
    [AnimalId] int NOT NULL,
    CONSTRAINT [PK_Surgeries] PRIMARY KEY ([SurgeryId]),
    CONSTRAINT [FK_Surgeries_Animals_AnimalId] FOREIGN KEY ([AnimalId]) REFERENCES [Animals] ([AnimalId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Surgeries_Inscriptions_InscriptionId] FOREIGN KEY ([InscriptionId]) REFERENCES [Inscriptions] ([InscriptionId])
);
GO

CREATE TABLE [Vaccines] (
    [VaccineId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AplicationDate] datetime2 NOT NULL,
    [InscriptionId] int NULL,
    [AnimalId] int NOT NULL,
    CONSTRAINT [PK_Vaccines] PRIMARY KEY ([VaccineId]),
    CONSTRAINT [FK_Vaccines_Animals_AnimalId] FOREIGN KEY ([AnimalId]) REFERENCES [Animals] ([AnimalId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vaccines_Inscriptions_InscriptionId] FOREIGN KEY ([InscriptionId]) REFERENCES [Inscriptions] ([InscriptionId])
);
GO

CREATE INDEX [IX_Animals_CustomerId] ON [Animals] ([CustomerId]);
GO

CREATE INDEX [IX_Animals_TypeAnimalId] ON [Animals] ([TypeAnimalId]);
GO

CREATE INDEX [IX_Appointments_AnimalId] ON [Appointments] ([AnimalId]);
GO

CREATE INDEX [IX_Appointments_InscriptionId] ON [Appointments] ([InscriptionId]);
GO

CREATE INDEX [IX_Cantons_ProvinceId] ON [Cantons] ([ProvinceId]);
GO

CREATE INDEX [IX_Customers_SexId] ON [Customers] ([SexId]);
GO

CREATE INDEX [IX_Diagnostics_AnimalId] ON [Diagnostics] ([AnimalId]);
GO

CREATE INDEX [IX_Diagnostics_InscriptionId] ON [Diagnostics] ([InscriptionId]);
GO

CREATE INDEX [IX_Directions_ClinicId] ON [Directions] ([ClinicId]);
GO

CREATE INDEX [IX_Directions_CustomerId] ON [Directions] ([CustomerId]);
GO

CREATE INDEX [IX_Directions_DistrictId] ON [Directions] ([DistrictId]);
GO

CREATE INDEX [IX_Districts_CantonId] ON [Districts] ([CantonId]);
GO

CREATE INDEX [IX_Inscriptions_ClinicId] ON [Inscriptions] ([ClinicId]);
GO

CREATE INDEX [IX_Inscriptions_VeterinarianId] ON [Inscriptions] ([VeterinarianId]);
GO

CREATE INDEX [IX_Recipes_AnimalId] ON [Recipes] ([AnimalId]);
GO

CREATE INDEX [IX_Recipes_InscriptionId] ON [Recipes] ([InscriptionId]);
GO

CREATE INDEX [IX_Surgeries_AnimalId] ON [Surgeries] ([AnimalId]);
GO

CREATE INDEX [IX_Surgeries_InscriptionId] ON [Surgeries] ([InscriptionId]);
GO

CREATE INDEX [IX_Vaccines_AnimalId] ON [Vaccines] ([AnimalId]);
GO

CREATE INDEX [IX_Vaccines_InscriptionId] ON [Vaccines] ([InscriptionId]);
GO

CREATE INDEX [IX_veterinarians_SexId] ON [veterinarians] ([SexId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231211024109_InitialCreate', N'7.0.14');
GO

COMMIT;
GO

