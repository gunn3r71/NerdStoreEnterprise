﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStoreEnterprise.Services.Customer.API.Data;

namespace NerdStoreEnterprise.Services.Customer.API.Data.CustomersMigrations
{
    [DbContext(typeof(CustomersDbContext))]
    partial class CustomersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity("NerdStoreEnterprise.Services.Customer.API.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AddressComplement")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("CHAR(36)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<int>("TempId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("TempId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("NerdStoreEnterprise.Services.Customer.API.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)");

                    b.Property<int>("TempId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("TempId1");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NerdStoreEnterprise.Services.Customer.API.Models.Address", b =>
                {
                    b.HasOne("NerdStoreEnterprise.Services.Customer.API.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("NerdStoreEnterprise.Services.Customer.API.Models.Customer", b =>
                {
                    b.OwnsOne("NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("VARCHAR(11)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsOne("NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("VARCHAR(254)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Cpf");

                    b.Navigation("Email");
                });
#pragma warning restore 612, 618
        }
    }
}
