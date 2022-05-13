using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.Products;
using Refit;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public interface ICatalogService
    {
        [Get("/catalog/products")]
        Task<IEnumerable<ProductViewModel>> GetAll();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> GetById(Guid id);
    }
}