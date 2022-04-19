using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Models.Products;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient,
                              IOptions<ServicesUrls> serviceUrls)
        {
            httpClient.BaseAddress = new Uri(serviceUrls.Value.CatalogUrl);

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/v1/Catalog/products");

            HandleResponse(response);

            return await DeserializeResponseAsync<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/products/{id}");

            HandleResponse(response);

            return await DeserializeResponseAsync<ProductViewModel>(response);
        }
    }
}