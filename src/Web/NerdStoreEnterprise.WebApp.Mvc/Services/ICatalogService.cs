using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.Products;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
    }
}