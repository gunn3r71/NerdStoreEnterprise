using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.EF
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        ///     Will configure unmapped strings
        /// </summary>
        /// <param name="builder">modelBuilder</param>
        public static void ConfigureUnmappedStrings(this ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        /// <summary>
        ///     Will disable cascade deletion
        /// </summary>
        /// <param name="builder">modelBuilder</param>
        public static void DisableDeleteBehavior(this ModelBuilder builder)
        {
            foreach (var relationShip in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationShip.DeleteBehavior = DeleteBehavior.SetNull;
        }

        /// <summary>
        ///     Will ignore domain events when trying to persist data
        /// </summary>
        /// <param name="builder"></param>
        public static void IgnoreDomainMessageItems(this ModelBuilder builder)
        {
            builder.Ignore<ValidationResult>();
            builder.Ignore<Event>();
        }
    }
}