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
    [HashPassword] nvarchar(max) NOT NULL,
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
    [AplicationDate] int NOT NULL,
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
VALUES (N'20231203000614_FirstMigration', N'7.0.14');
GO

COMMIT;
GO

-- Insert data into Clinics table
INSERT INTO Clinics (Name, PhoneNumber, URLLogo, Email, UserName, HashPassword, IsActive)
VALUES 
    ('AnimalCare Clinic', 123456789, 'https://www.example.com/logo1.png', 'info@animalcare.com', 'animalcare_user', 'hash123', 1),
    ('PetWellness Center', 987654321, 'https://www.example.com/logo2.png', 'info@petwellness.com', 'petwellness_user', 'hash456', 1),
    ('HealthyPaws Veterinary', 555111222, 'https://www.example.com/logo3.png', 'info@healthypaws.com', 'healthypaws_user', 'hash789', 1);

-- Insert data into Provinces table
INSERT INTO Provinces (Name)
VALUES 
    ('Province A'),
    ('Province B'),
    ('Province C');

-- Insert data into Sexes table
INSERT INTO Sexes (Name)
VALUES 
    ('Male'),
    ('Female'),
    ('Other');

-- Insert data into TypeAnimals table
INSERT INTO TypeAnimals (TypeName)
VALUES 
    ('Dog'),
    ('Cat'),
    ('Bird');

-- Insert data into Cantons table
INSERT INTO Cantons (Name, ProvinceId)
VALUES 
    ('Canton 1', 1),
    ('Canton 2', 1),
    ('Canton 3', 2);

-- Insert data into Customers table
INSERT INTO Customers (UserName, HashPassword, DNI, IdentificationType, Name, LastName, SecondLastName, PhoneNumber, Email, SexId)
VALUES 
    ('customer1', 'customer123', 123456789, 'ID', 'John', 'Doe', 'Smith', 987654321, 'john.doe@email.com', 1),
    ('customer2', 'customer456', 987654321, 'ID', 'Jane', 'Smith', 'Doe', 555111222, 'jane.smith@email.com', 2),
    ('customer3', 'customer789', 555111222, 'ID', 'Bob', 'Johnson', 'Williams', 123456789, 'bob.johnson@email.com', 3);

-- Insert data into veterinarians table
INSERT INTO veterinarians (DNI, IdentificationType, Name, LastName, SecondLastName, PhoneNumber, Email, SexId)
VALUES 
    (123456789, 'ID', 'Dr. Sarah', 'Jones', 'Lee', 987654321, 'sarah.jones@email.com', 2),
    (987654321, 'ID', 'Dr. Michael', 'Smith', 'Johnson', 555111222, 'michael.smith@email.com', 1),
    (555111222, 'ID', 'Dr. Emily', 'Davis', 'Brown', 123456789, 'emily.davis@email.com', 2);

-- Insert data into Districts table
INSERT INTO Districts (Name, CantonId)
VALUES 
    ('District 1', 1),
    ('District 2', 1),
    ('District 3', 2);

-- Insert data into Animals table
INSERT INTO Animals (Name, URLImage, IsActive, CustomerId, TypeAnimalId)
VALUES 
    ('Buddy', 'https://www.example.com/dog.jpg', 1, 1, 1),
    ('Whiskers', 'https://www.example.com/cat.jpg', 1, 2, 2),
    ('Tweety', 'https://www.example.com/bird.jpg', 1, 3, 3);

-- Insert data into Inscriptions table
INSERT INTO Inscriptions (VeterinarianId, ClinicId)
VALUES 
    (1, 1),
    (2, 2),
    (3, 3);

-- Insert data into Directions table
INSERT INTO Directions (DirectionDescription, DistrictId, PersonId, CustomerId, ClinicId)
VALUES 
    ('123 Main St', 1, 1, 1, 1),
    ('456 Oak Ave', 2, 2, 2, 2),
    ('789 Pine Blvd', 3, 3, 3, 1);

-- Insert data into Appointments table
INSERT INTO Appointments (DateToMeet, InscriptionId, AnimalId)
VALUES 
    ('2023-01-01 10:00:00', 1, 1),
    ('2023-02-15 14:30:00', 2, 2),
    ('2023-03-20 16:45:00', 3, 3);

-- Insert data into Diagnostics table
INSERT INTO Diagnostics (Title, InscriptionId, AnimalId)
VALUES 
    ('Checkup', 1, 1),
    ('Vaccination', 2, 2),
    ('X-ray', 3, 3);

-- Insert data into Recipes table
INSERT INTO Recipes (Title, CreationDate, Indications, InscriptionId, AnimalId)
VALUES 
    ('Healthy Diet', '2023-01-02 08:00:00', 'Feed twice a day', 1, 1),
    ('Medication Schedule', '2023-02-16 10:30:00', 'Follow prescription', 2, 2),
    ('Special Diet', '2023-03-21 12:15:00', 'No treats', 3, 3);

-- Insert data into Surgeries table
INSERT INTO Surgeries (Title, Creation, Description, InscriptionId, AnimalId)
VALUES 
    ('Spaying', '2023-01-03 09:30:00', 'Routine spaying procedure', 1, 1),
    ('Dental Procedure', '2023-02-17 11:00:00', 'Cleaning and extraction', 2, 2),
    ('Fracture Repair', '2023-03-22 14:00:00', 'Orthopedic surgery', 3, 3);

-- Insert data into Vaccines table
INSERT INTO Vaccines (Title, AplicationDate, InscriptionId, AnimalId)
VALUES 
    ('Rabies Vaccine', 14, 1, 1),
    ('Feline Leukemia Vaccine', 28, 2, 2),
    ('Avian Influenza Vaccine', 21, 3, 3);


commit;
go