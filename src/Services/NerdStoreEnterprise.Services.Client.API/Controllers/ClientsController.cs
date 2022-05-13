using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Controllers;
using NerdStoreEnterprise.Services.Client.API.Application.Commands;

namespace NerdStoreEnterprise.Services.Client.API.Controllers
{
    [Route("api/v1/clients")]
    public class ClientsController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediatorHandler.SendCommand(new CreateClientCommand(Guid.NewGuid(), "Lucas", "lucas.p.oliveira@outlook.com", "51566495806"));

            return CustomResponse(result);
        }
    }
}
