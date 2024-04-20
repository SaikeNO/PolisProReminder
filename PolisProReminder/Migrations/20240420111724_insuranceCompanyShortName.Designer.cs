﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolisProReminder.Entities;

#nullable disable

namespace PolisProReminder.Migrations
{
    [DbContext(typeof(InsuranceDbContext))]
    [Migration("20240420111724_insuranceCompanyShortName")]
    partial class insuranceCompanyShortName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InsuranceTypePolicy", b =>
                {
                    b.Property<int>("InsuranceTypesId")
                        .HasColumnType("int");

                    b.Property<int>("PoliciesId")
                        .HasColumnType("int");

                    b.HasKey("InsuranceTypesId", "PoliciesId");

                    b.HasIndex("PoliciesId");

                    b.ToTable("InsuranceTypePolicy");
                });

            modelBuilder.Entity("PolisProReminder.Entities.InsuranceCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InsuranceCompanies", (string)null);
                });

            modelBuilder.Entity("PolisProReminder.Entities.InsuranceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("InsuranceTypes", (string)null);
                });

            modelBuilder.Entity("PolisProReminder.Entities.Insurer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Insurers", (string)null);
                });

            modelBuilder.Entity("PolisProReminder.Entities.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InsuranceCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("InsurerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InsuranceCompanyId");

                    b.HasIndex("InsurerId");

                    b.ToTable("Policies", (string)null);
                });

            modelBuilder.Entity("PolisProReminder.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("PolisProReminder.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PolisProReminder.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("SuperiorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("SuperiorId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InsuranceTypePolicy", b =>
                {
                    b.HasOne("PolisProReminder.Entities.InsuranceType", null)
                        .WithMany()
                        .HasForeignKey("InsuranceTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolisProReminder.Entities.Policy", null)
                        .WithMany()
                        .HasForeignKey("PoliciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PolisProReminder.Entities.Policy", b =>
                {
                    b.HasOne("PolisProReminder.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolisProReminder.Entities.InsuranceCompany", "InsuranceCompany")
                        .WithMany("Policies")
                        .HasForeignKey("InsuranceCompanyId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PolisProReminder.Entities.Insurer", "Insurer")
                        .WithMany("Policies")
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("InsuranceCompany");

                    b.Navigation("Insurer");
                });

            modelBuilder.Entity("PolisProReminder.Entities.RefreshToken", b =>
                {
                    b.HasOne("PolisProReminder.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("PolisProReminder.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolisProReminder.Entities.User", b =>
                {
                    b.HasOne("PolisProReminder.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolisProReminder.Entities.User", "Superior")
                        .WithMany()
                        .HasForeignKey("SuperiorId");

                    b.Navigation("Role");

                    b.Navigation("Superior");
                });

            modelBuilder.Entity("PolisProReminder.Entities.InsuranceCompany", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("PolisProReminder.Entities.Insurer", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("PolisProReminder.Entities.User", b =>
                {
                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}
