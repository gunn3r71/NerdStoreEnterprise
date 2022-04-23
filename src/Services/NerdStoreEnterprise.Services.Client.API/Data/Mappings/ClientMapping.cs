using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects;

namespace NerdStoreEnterprise.Services.Client.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Models.Client>
    {
        public void Configure(EntityTypeBuilder<Models.Client> builder)
        {
            builder.HasKey(client => client.Id);

            builder.Property(client => client.Name)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("varchar(120)");

            builder.OwnsOne(client => client.Cpf, cpf =>
            {
                cpf.Property(x => x.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.Length)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.Length})");
            });

            builder.OwnsOne(client => client.Email, email =>
            {
                email.Property(x => x.Address)
                    .IsRequired()
                    .HasMaxLength(Email.MaxLength)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.MaxLength})");
            });

            builder.HasOne(client => client.Address)
                .WithOne(address => address.Client);

            builder.ToTable("Clients");
        }
    }
}