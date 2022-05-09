using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;


namespace NerdStoreEnterprise.Services.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> FindById(Guid id);
        void Add(Product product);
        void Update(Product product);
    }
}