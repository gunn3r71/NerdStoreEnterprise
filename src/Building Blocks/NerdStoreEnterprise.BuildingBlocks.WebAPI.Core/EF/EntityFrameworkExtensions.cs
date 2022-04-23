using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.EF
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Will configure unmapped strings
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        public static void ConfigureUnmappedStrings(this ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        /// <summary>
        /// Will disable cascade deletion
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        public static void DisableDeleteBehavior(this ModelBuilder modelBuilder)
        {
            foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationShip.DeleteBehavior = DeleteBehavior.SetNull;
        }
    }
}