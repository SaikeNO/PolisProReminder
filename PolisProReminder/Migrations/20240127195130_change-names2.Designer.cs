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
    [Migration("20240127195130_change-names2")]
    partial class changenames2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("InsuranceCompanies", (string)null);
                });

            modelBuilder.Entity("PolisProReminder.Entities.InsuranceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("InsurerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceCompanyId");

                    b.HasIndex("InsurerId");

                    b.ToTable("Policies", (string)null);
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
                    b.HasOne("PolisProReminder.Entities.InsuranceCompany", "InsuranceCompany")
                        .WithMany("Policies")
                        .HasForeignKey("InsuranceCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolisProReminder.Entities.Insurer", "Insurer")
                        .WithMany("Policies")
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceCompany");

                    b.Navigation("Insurer");
                });

            modelBuilder.Entity("PolisProReminder.Entities.InsuranceCompany", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("PolisProReminder.Entities.Insurer", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
