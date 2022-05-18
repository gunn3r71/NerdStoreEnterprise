using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NerdStoreEnterprise.Services.Catalog.API.Data;
using NerdStoreEnterprise.Services.Catalog.API.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Infrastructure.Seeders
{
    public class ProductSeeder : IProductSeeder
    {
        private readonly CatalogDbContext _context;

        public ProductSeeder(CatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Seed()
        {
            if (HasData()) return;

            _context.AddRange(GetInitialProducts());

            _context.SaveChanges();
        }

        public async Task SeedAsync()
        {
            if (HasData()) return;

            _context.AddRange(GetInitialProducts());

            await _context.SaveChangesAsync();
        }

        private bool HasData() => _context.Products.Any();

        private static IEnumerable<Product> GetInitialProducts()
        {
            return new List<Product>
            {
                new("MacBook PRO M1", "home-office", true, 20000.00M, DateTime.Now, "", 1000),
                new("Notebook gamer i7", "gamer", true, 10000.00M, DateTime.Now, "", 2000),
                new("Notebook gamer i5", "gamer", true, 2000.00M, DateTime.Now, "", 5000),
                new("Notebook gamer i3", "gamer", false, 15000.00M, DateTime.Now, "", 500)
            };
        }
    }
}