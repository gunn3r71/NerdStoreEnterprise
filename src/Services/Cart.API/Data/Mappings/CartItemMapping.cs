using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Data.Mappings
{
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Image)
                .HasMaxLength(300);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasColumnType("CHAR(36)")
                .IsRequired();
        }
    }
}