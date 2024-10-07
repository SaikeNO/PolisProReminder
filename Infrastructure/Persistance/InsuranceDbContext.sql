USE [master]
GO
/****** Object:  Database [PolisProReminder]    Script Date: 07.10.2024 21:43:17 ******/
CREATE DATABASE [PolisProReminder]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PolisProReminder', FILENAME = N'C:\Users\mateu\PolisProReminder.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PolisProReminder_log', FILENAME = N'C:\Users\mateu\PolisProReminder_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PolisProReminder] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PolisProReminder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PolisProReminder] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PolisProReminder] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PolisProReminder] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PolisProReminder] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PolisProReminder] SET ARITHABORT OFF 
GO
ALTER DATABASE [PolisProReminder] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PolisProReminder] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PolisProReminder] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PolisProReminder] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PolisProReminder] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PolisProReminder] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PolisProReminder] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PolisProReminder] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PolisProReminder] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PolisProReminder] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PolisProReminder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PolisProReminder] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PolisProReminder] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PolisProReminder] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PolisProReminder] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PolisProReminder] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PolisProReminder] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PolisProReminder] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PolisProReminder] SET  MULTI_USER 
GO
ALTER DATABASE [PolisProReminder] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PolisProReminder] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PolisProReminder] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PolisProReminder] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PolisProReminder] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PolisProReminder] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PolisProReminder] SET QUERY_STORE = OFF
GO
USE [PolisProReminder]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[AgentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[UniqueFileName] [nvarchar](255) NOT NULL,
	[FilePath] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[PolicyId] [uniqueidentifier] NULL,
	[VehicleId] [uniqueidentifier] NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsuranceCompanies]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceCompanies](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InsuranceCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsuranceTypePolicy]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceTypePolicy](
	[InsuranceTypesId] [uniqueidentifier] NOT NULL,
	[PoliciesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InsuranceTypePolicy] PRIMARY KEY CLUSTERED 
(
	[InsuranceTypesId] ASC,
	[PoliciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsuranceTypes]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InsuranceTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insurers]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](60) NULL,
	[LastName] [nvarchar](60) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Email] [nvarchar](60) NULL,
	[Pesel] [nvarchar](11) NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[City] [nvarchar](60) NULL,
	[PostalCode] [nvarchar](6) NULL,
	[Street] [nvarchar](60) NULL,
	[InsurerType] [nvarchar](13) NOT NULL,
	[Name] [nvarchar](60) NULL,
	[Nip] [nvarchar](10) NULL,
	[Regon] [nvarchar](9) NULL,
 CONSTRAINT [PK_Insurers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](60) NOT NULL,
	[PolicyNumber] [nvarchar](60) NOT NULL,
	[InsurerId] [uniqueidentifier] NOT NULL,
	[InsuranceCompanyId] [uniqueidentifier] NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[PaymentDate] [date] NULL,
	[IsPaid] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[VehicleId] [uniqueidentifier] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleBrands]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleBrands](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_VehicleBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 07.10.2024 21:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[RegistrationNumber] [nvarchar](7) NOT NULL,
	[FirstRegistrationDate] [date] NULL,
	[VIN] [nvarchar](17) NULL,
	[InsurerId] [uniqueidentifier] NOT NULL,
	[CreatedByAgentId] [uniqueidentifier] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Capacity] [int] NULL,
	[KM] [int] NULL,
	[KW] [int] NULL,
	[Mileage] [bigint] NULL,
	[ProductionYear] [date] NULL,
	[VehicleBrandId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 07.10.2024 21:43:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 07.10.2024 21:43:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Attachments_PolicyId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Attachments_PolicyId] ON [dbo].[Attachments]
(
	[PolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Attachments_VehicleId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Attachments_VehicleId] ON [dbo].[Attachments]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_InsuranceTypePolicy_PoliciesId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_InsuranceTypePolicy_PoliciesId] ON [dbo].[InsuranceTypePolicy]
(
	[PoliciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Policies_InsuranceCompanyId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Policies_InsuranceCompanyId] ON [dbo].[Policies]
(
	[InsuranceCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Policies_InsurerId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Policies_InsurerId] ON [dbo].[Policies]
(
	[InsurerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Policies_VehicleId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Policies_VehicleId] ON [dbo].[Policies]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_InsurerId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_InsurerId] ON [dbo].[Vehicles]
(
	[InsurerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_VehicleBrandId]    Script Date: 07.10.2024 21:43:17 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_VehicleBrandId] ON [dbo].[Vehicles]
(
	[VehicleBrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attachments] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Insurers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Insurers] ADD  DEFAULT (N'Individual') FOR [InsurerType]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [VehicleBrandId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Policies_PolicyId] FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([Id])
GO
ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Policies_PolicyId]
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Vehicles_VehicleId]
GO
ALTER TABLE [dbo].[InsuranceTypePolicy]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceTypePolicy_InsuranceTypes_InsuranceTypesId] FOREIGN KEY([InsuranceTypesId])
REFERENCES [dbo].[InsuranceTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InsuranceTypePolicy] CHECK CONSTRAINT [FK_InsuranceTypePolicy_InsuranceTypes_InsuranceTypesId]
GO
ALTER TABLE [dbo].[InsuranceTypePolicy]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceTypePolicy_Policies_PoliciesId] FOREIGN KEY([PoliciesId])
REFERENCES [dbo].[Policies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InsuranceTypePolicy] CHECK CONSTRAINT [FK_InsuranceTypePolicy_Policies_PoliciesId]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_InsuranceCompanies_InsuranceCompanyId] FOREIGN KEY([InsuranceCompanyId])
REFERENCES [dbo].[InsuranceCompanies] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_InsuranceCompanies_InsuranceCompanyId]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Insurers_InsurerId] FOREIGN KEY([InsurerId])
REFERENCES [dbo].[Insurers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Insurers_InsurerId]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Vehicles_VehicleId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Insurers_InsurerId] FOREIGN KEY([InsurerId])
REFERENCES [dbo].[Insurers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Insurers_InsurerId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_VehicleBrands_VehicleBrandId] FOREIGN KEY([VehicleBrandId])
REFERENCES [dbo].[VehicleBrands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_VehicleBrands_VehicleBrandId]
GO
USE [master]
GO
ALTER DATABASE [PolisProReminder] SET  READ_WRITE 
GO
