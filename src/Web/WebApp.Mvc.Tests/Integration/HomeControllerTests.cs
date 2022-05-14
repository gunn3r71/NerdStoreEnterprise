using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NerdStoreEnterprise.WebApp.Mvc;

namespace WebApp.Mvc.Tests.Integration
{
    internal class HomeControllerTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        public HomeControllerTests(HttpClient httpClient)
        {
            //_testServer = new TestServer(WebHost.CreateDefaultBuilder<Startup>().UseStartup(Startup));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    }
}
