# PolisProReminder

## Wprowadzenie
Projekt PolisProReminder to backend aplikacji webowej, który został zbudowany przy użyciu technologii .NET Web API. Celem projektu jest dostarczenie efektywnego API do komunikacji między aplikacją internetową. Aplikacja obsługuje zarządzanie danymi, autoryzację użytkowników oraz udostępnia zestaw funkcji poprzez API.

Aplikacja została zaprojektowana z myślą o agentach ubezpieczeniowych. Celem projektu jest umożliwienie sprawnego zarządzania polisami klientów, usprawniając procesy obsługi ubezpieczeń.

## Technologie
Projekt wykorzystuje następujące technologie:
- **ASP.NET Core**: Framework umożliwiający budowę aplikacji internetowych w języku C#.
- **Entity Framework Core**: Narzędzie do efektywnego zarządzania danymi związanymi z polisami ubezpieczeniowymi w bazie danych.
- **Swagger**: Generuje czytelną dokumentację API, ułatwiającą korzystanie z funkcji systemu.

## Funkcjonalności
### 1. Zarządzanie Polisami
- Dodawanie, edycja i usuwanie polis ubezpieczeniowych klientów.
- Dodawanie, edycja i usuwanie klientów.
- Dodawanie, edycja i usuwanie towarzystw ubezpieczeniowych.

### 2. Autoryzacja i Bezpieczeństwo
- Autentykacja agentów poprzez tokeny JWT, zabezpieczając dostęp do poufnych danych.
- Hierarchia ról, umożliwiająca różnicowanie uprawnień w zależności od roli agenta.

## Kontrolery
![alt text](swagger.png "Swagger")
### 1. AccountController
`AccountController` jest dedykowany procesowi logowania do aplikacji. Zapewnia interfejs umożliwiający agentom ubezpieczeniowym autoryzację poprzez dostarczenie danych uwierzytelniających. Kontroler ten nie oferuje funkcji rejestracji, a jedynie umożliwia zalogowanie się do systemu.

### 2. InsuranceCompanyController
Kontroler `InsuranceCompany` obsługuje operacje związane z zarządzaniem firmami ubezpieczeniowymi. Zapewnia funkcje CRUD (Create, Read, Update, Delete) umożliwiające dodawanie, edycję, usuwanie oraz przegląd informacji o poszczególnych firmach ubezpieczeniowych.

### 3. InsuranceTypeController
Kontroler `InsuranceType` odpowiada za operacje związane z rodzajami polis ubezpieczeniowych. Umożliwia dodawanie nowych typów polis, edycję istniejących oraz przegląd dostępnych rodzajów ubezpieczeń.

### 4. InsurerController
`InsurerController` zajmuje się zarządzaniem informacjami na temat ubezpieczających. Dostarcza interfejs do dodawania, edycji, usuwania oraz przeglądania danych o klientach posiadających polisy ubezpieczeniowe.

### 5. PolicyController
Kontroler `Policy` obsługuje operacje związane z polisami ubezpieczeniowymi. Zapewnia funkcje CRUD do zarządzania polisami, umożliwiając agentom ubezpieczeniowym dodawanie, edycję, usuwanie oraz przegląd szczegółów dotyczących polis ich klientów.

## Middleware
Aplikacja wykorzystuje middleware do przechwytywania wyjątków, co pozwala na kontrolowane i jednolite zarządzanie błędami w systemie. Middleware ten gwarantuje, że nawet w przypadku wystąpienia nieoczekiwanego wyjątku, użytkownicy otrzymają odpowiedzi błędów z odpowiednimi komunikatami, a jednocześnie logi aplikacji zawierają pełne informacje diagnostyczne.

## Diagram Bazy Danych
![alt text](database-diagram.png "Diagram bazy danych")

## Kod SQL - DDL Bazy Danych
Poniżej znajduje się kod SQL zawierający DDL dla struktury bazy danych:

``` sql
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
```