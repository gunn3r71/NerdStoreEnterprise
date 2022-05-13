﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Customer.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.Deleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.OwnsOne(x => x.Cpf, c =>
            {
                c.Property(x => x.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.Length)
                    .HasColumnType($"VARCHAR({Cpf.Length})");
            });

            builder.OwnsOne(x => x.Email, c =>
            {
                c.Property(x => x.Address)
                    .IsRequired()
                    .HasMaxLength(Email.MaxLength)
                    .HasColumnType($"VARCHAR({Email.MaxLength})");
            });

            builder.HasOne(x => x.Address)
                .WithOne(x => x.Customer);
        }
    }
}