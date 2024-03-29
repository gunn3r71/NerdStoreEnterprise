﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Services.Catalog.API.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(x => x.Id)
                .HasColumnType("CHAR(36)")
                .IsRequired();

            builder.Property(product => product.Name)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(product => product.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(product => product.Image)
                .IsRequired(false);

            builder.Property(product => product.QuantityInStock)
                .IsRequired();

            builder.Property(product => product.Price)
                .IsRequired();

            builder.Property(product => product.CreatedAt)
                .IsRequired();

            builder.Property(product => product.Status)
                .IsRequired();
        }
    }
}