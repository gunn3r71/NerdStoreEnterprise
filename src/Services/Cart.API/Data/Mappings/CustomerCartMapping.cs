using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Data.Mappings
{
    public class CustomerCartMapping : IEntityTypeConfiguration<CustomerCart>
    {
        public void Configure(EntityTypeBuilder<CustomerCart> builder)
        {
            builder.ToTable("CustomerCarts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Total)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId);

            builder.HasIndex(x => x.CustomerId)
                .HasDatabaseName("IDX_Customer_Cart");
        }
    }
}