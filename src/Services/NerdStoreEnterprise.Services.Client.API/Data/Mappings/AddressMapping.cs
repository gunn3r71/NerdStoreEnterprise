using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Services.Client.API.Models;

namespace NerdStoreEnterprise.Services.Client.API.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Adresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StreetName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.AddressComplement)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.BuildingNumber)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("VARCHAR(10)");

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");
        }
    }
}