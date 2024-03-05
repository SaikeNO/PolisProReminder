# PolisProReminder

## Wprowadzenie
Projekt PolisProReminder to backend aplikacji webowej, kt�ry zosta� zbudowany przy u�yciu technologii .NET Web API. Celem projektu jest dostarczenie efektywnego API do komunikacji mi�dzy aplikacj� internetow�. Aplikacja obs�uguje zarz�dzanie danymi, autoryzacj� u�ytkownik�w oraz udost�pnia zestaw funkcji poprzez API.

Aplikacja zosta�a zaprojektowana z my�l� o agentach ubezpieczeniowych. Celem projektu jest umo�liwienie sprawnego zarz�dzania polisami klient�w, usprawniaj�c procesy obs�ugi ubezpiecze�.

## Technologie
Projekt wykorzystuje nast�puj�ce technologie:
- **ASP.NET Core**: Framework umo�liwiaj�cy budow� aplikacji internetowych w j�zyku C#.
- **Entity Framework Core**: Narz�dzie do efektywnego zarz�dzania danymi zwi�zanymi z polisami ubezpieczeniowymi w bazie danych.
- **Swagger**: Generuje czyteln� dokumentacj� API, u�atwiaj�c� korzystanie z funkcji systemu.

## Funkcjonalno�ci
### 1. Zarz�dzanie Polisami
- Dodawanie, edycja i usuwanie polis ubezpieczeniowych klient�w.
- Dodawanie, edycja i usuwanie klient�w.
- Dodawanie, edycja i usuwanie towarzystw ubezpieczeniowych.

### 2. Autoryzacja i Bezpiecze�stwo
- Autentykacja agent�w poprzez tokeny JWT, zabezpieczaj�c dost�p do poufnych danych.
- Hierarchia r�l, umo�liwiaj�ca r�nicowanie uprawnie� w zale�no�ci od roli agenta.

## Kontrolery
![alt text](swagger.png "Swagger")
### 1. AccountController
`AccountController` jest dedykowany procesowi logowania do aplikacji. Zapewnia interfejs umo�liwiaj�cy agentom ubezpieczeniowym autoryzacj� poprzez dostarczenie danych uwierzytelniaj�cych. Kontroler ten nie oferuje funkcji rejestracji, a jedynie umo�liwia zalogowanie si� do systemu.

### 2. InsuranceCompanyController
Kontroler `InsuranceCompany` obs�uguje operacje zwi�zane z zarz�dzaniem firmami ubezpieczeniowymi. Zapewnia funkcje CRUD (Create, Read, Update, Delete) umo�liwiaj�ce dodawanie, edycj�, usuwanie oraz przegl�d informacji o poszczeg�lnych firmach ubezpieczeniowych.

### 3. InsuranceTypeController
Kontroler `InsuranceType` odpowiada za operacje zwi�zane z rodzajami polis ubezpieczeniowych. Umo�liwia dodawanie nowych typ�w polis, edycj� istniej�cych oraz przegl�d dost�pnych rodzaj�w ubezpiecze�.

### 4. InsurerController
`InsurerController` zajmuje si� zarz�dzaniem informacjami na temat ubezpieczaj�cych. Dostarcza interfejs do dodawania, edycji, usuwania oraz przegl�dania danych o klientach posiadaj�cych polisy ubezpieczeniowe.

### 5. PolicyController
Kontroler `Policy` obs�uguje operacje zwi�zane z polisami ubezpieczeniowymi. Zapewnia funkcje CRUD do zarz�dzania polisami, umo�liwiaj�c agentom ubezpieczeniowym dodawanie, edycj�, usuwanie oraz przegl�d szczeg��w dotycz�cych polis ich klient�w.

## Middleware
Aplikacja wykorzystuje middleware do przechwytywania wyj�tk�w, co pozwala na kontrolowane i jednolite zarz�dzanie b��dami w systemie. Middleware ten gwarantuje, �e nawet w przypadku wyst�pienia nieoczekiwanego wyj�tku, u�ytkownicy otrzymaj� odpowiedzi b��d�w z odpowiednimi komunikatami, a jednocze�nie logi aplikacji zawieraj� pe�ne informacje diagnostyczne.

## Diagram Bazy Danych
![alt text](database-diagram.png "Diagram bazy danych")

## Kod SQL - DDL Bazy Danych
Poni�ej znajduje si� kod SQL zawieraj�cy DDL dla struktury bazy danych:

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