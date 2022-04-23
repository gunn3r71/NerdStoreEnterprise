using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Services.Client.API.Models;

namespace NerdStoreEnterprise.Services.Client.API.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);
            
            builder.Property(address => address.StreetName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
            
            builder.Property(address => address.BuildingNumber)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");
            
            builder.Property(address => address.AddressComplement)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
            
            builder.Property(address => address.ZipCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnType("varchar(8)");
            
            builder.Property(address => address.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");
            
            builder.Property(address => address.State)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnType("varchar(2)");

            builder.ToTable("Addresses");
        }
    }
}