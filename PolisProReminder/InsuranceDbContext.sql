CREATE TABLE [InsuranceCompanies] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_InsuranceCompanies] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [InsuranceTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_InsuranceTypes] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Insurers] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(20) NOT NULL,
    [LastName] nvarchar(20) NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Pesel] nvarchar(11) NOT NULL,
    CONSTRAINT [PK_Insurers] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [RoleId] int NOT NULL,
    [SuperiorId] int NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_Users_SuperiorId] FOREIGN KEY ([SuperiorId]) REFERENCES [Users] ([Id])
);
GO


CREATE TABLE [Policies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(60) NOT NULL,
    [PolicyNumber] nvarchar(60) NOT NULL,
    [InsurerId] int NOT NULL,
    [InsuranceCompanyId] int NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [PaymentDate] datetime2 NOT NULL,
    [IsPaid] bit NOT NULL,
    [CreatedById] int NOT NULL,
    CONSTRAINT [PK_Policies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Policies_InsuranceCompanies_InsuranceCompanyId] FOREIGN KEY ([InsuranceCompanyId]) REFERENCES [InsuranceCompanies] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Policies_Insurers_InsurerId] FOREIGN KEY ([InsurerId]) REFERENCES [Insurers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Policies_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [InsuranceTypePolicy] (
    [InsuranceTypesId] int NOT NULL,
    [PoliciesId] int NOT NULL,
    CONSTRAINT [PK_InsuranceTypePolicy] PRIMARY KEY ([InsuranceTypesId], [PoliciesId]),
    CONSTRAINT [FK_InsuranceTypePolicy_InsuranceTypes_InsuranceTypesId] FOREIGN KEY ([InsuranceTypesId]) REFERENCES [InsuranceTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_InsuranceTypePolicy_Policies_PoliciesId] FOREIGN KEY ([PoliciesId]) REFERENCES [Policies] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_InsuranceTypePolicy_PoliciesId] ON [InsuranceTypePolicy] ([PoliciesId]);
GO


CREATE INDEX [IX_Policies_CreatedById] ON [Policies] ([CreatedById]);
GO


CREATE INDEX [IX_Policies_InsuranceCompanyId] ON [Policies] ([InsuranceCompanyId]);
GO


CREATE INDEX [IX_Policies_InsurerId] ON [Policies] ([InsurerId]);
GO


CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO


CREATE INDEX [IX_Users_SuperiorId] ON [Users] ([SuperiorId]);
GO