using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.EF
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Will configure unmapped strings
        /// </summary>
        /// <param name="builder">modelBuilder</param>
        public static void ConfigureUnmappedStrings(this ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        /// <summary>
        /// Will disable cascade deletion
        /// </summary>
        /// <param name="builder">modelBuilder</param>
        public static void DisableDeleteBehavior(this ModelBuilder builder)
        {
            foreach (var relationShip in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationShip.DeleteBehavior = DeleteBehavior.SetNull;
        }
    }
}