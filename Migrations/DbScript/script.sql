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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProvinceId', N'Name') AND [object_id] = OBJECT_ID(N'[Provinces]'))
    SET IDENTITY_INSERT [Provinces] ON;
INSERT INTO [Provinces] ([ProvinceId], [Name])
VALUES (1, N'San José'),
(2, N'Alajuela'),
(3, N'Cartago'),
(4, N'Heredia'),
(5, N'Puntarenas'),
(6, N'Limón'),
(7, N'Guanacaste');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProvinceId', N'Name') AND [object_id] = OBJECT_ID(N'[Provinces]'))
    SET IDENTITY_INSERT [Provinces] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SexId', N'Name') AND [object_id] = OBJECT_ID(N'[Sexes]'))
    SET IDENTITY_INSERT [Sexes] ON;
INSERT INTO [Sexes] ([SexId], [Name])
VALUES (1, N'Hombre'),
(2, N'Mujer'),
(3, N'Otro');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SexId', N'Name') AND [object_id] = OBJECT_ID(N'[Sexes]'))
    SET IDENTITY_INSERT [Sexes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TypeAnimalId', N'TypeName') AND [object_id] = OBJECT_ID(N'[TypeAnimals]'))
    SET IDENTITY_INSERT [TypeAnimals] ON;
INSERT INTO [TypeAnimals] ([TypeAnimalId], [TypeName])
VALUES (1, N'Perro'),
(2, N'Gato'),
(3, N'Conejo'),
(4, N'Ave'),
(5, N'Cabra');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TypeAnimalId', N'TypeName') AND [object_id] = OBJECT_ID(N'[TypeAnimals]'))
    SET IDENTITY_INSERT [TypeAnimals] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CantonId', N'Name', N'ProvinceId') AND [object_id] = OBJECT_ID(N'[Cantons]'))
    SET IDENTITY_INSERT [Cantons] ON;
INSERT INTO [Cantons] ([CantonId], [Name], [ProvinceId])
VALUES (1, N'San José', 1),
(2, N'San Pedro', 1),
(3, N'Alajuela', 2),
(4, N'Grecia', 2),
(5, N'Cartago', 3),
(6, N'Paraíso', 3),
(7, N'Santo Domingo', 4),
(8, N'San Pablo', 4),
(9, N'Puntarenas', 5),
(10, N'Quepos', 5),
(11, N'Limón', 6),
(12, N'Guapiles', 6),
(13, N'Guanacaste', 7),
(14, N'Nicoya', 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CantonId', N'Name', N'ProvinceId') AND [object_id] = OBJECT_ID(N'[Cantons]'))
    SET IDENTITY_INSERT [Cantons] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'DNI', N'Email', N'HashPassword', N'IdentificationType', N'LastName', N'Name', N'PhoneNumber', N'SecondLastName', N'SexId', N'UserName') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] ON;
INSERT INTO [Customers] ([CustomerId], [DNI], [Email], [HashPassword], [IdentificationType], [LastName], [Name], [PhoneNumber], [SecondLastName], [SexId], [UserName])
VALUES (1, 11111, N'sample@mail.com', N'default', N'National', N'Prueba', N'nombre', 888888, N'prueba', 1, N'default');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'DNI', N'Email', N'HashPassword', N'IdentificationType', N'LastName', N'Name', N'PhoneNumber', N'SecondLastName', N'SexId', N'UserName') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DistrictId', N'CantonId', N'Name') AND [object_id] = OBJECT_ID(N'[Districts]'))
    SET IDENTITY_INSERT [Districts] ON;
INSERT INTO [Districts] ([DistrictId], [CantonId], [Name])
VALUES (1, 1, N'San Miguel'),
(2, 1, N'Escazú'),
(3, 2, N'San Pedro'),
(4, 2, N'San Rafael'),
(5, 3, N'Alajuela'),
(6, 3, N'San Ramón'),
(7, 4, N'Grecia'),
(8, 4, N'Sarchí'),
(9, 5, N'Cartago'),
(10, 5, N'Paraíso'),
(11, 6, N'Santo Domingo'),
(12, 6, N'San Vicente'),
(13, 7, N'Santo Domingo'),
(14, 7, N'San Juanillo'),
(15, 8, N'San Pablo'),
(16, 8, N'San Isidro'),
(17, 9, N'Puntarenas'),
(18, 9, N'Chacarita'),
(19, 10, N'Quepos'),
(20, 10, N'Parrita'),
(21, 11, N'Limón'),
(22, 11, N'Guácimo'),
(23, 12, N'Guápiles'),
(24, 12, N'Siquirres'),
(25, 13, N'Liberia'),
(26, 13, N'Santa Cruz'),
(27, 14, N'Nicoya'),
(28, 14, N'Santa Cruz');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DistrictId', N'CantonId', N'Name') AND [object_id] = OBJECT_ID(N'[Districts]'))
    SET IDENTITY_INSERT [Districts] OFF;
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
VALUES (N'20231217173708_InitialCreate', N'7.0.14');
GO

COMMIT;
GO

