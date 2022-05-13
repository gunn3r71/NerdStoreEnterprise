using System;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Controllers;

namespace NerdStoreEnterprise.Services.Customer.API.Controllers
{
    [Route("api/v1/clients")]
    public class ClientsController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }
    }
}