using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task PublishEventAsync<T>(T ev) where T : Event =>
            await _mediator.Publish(ev);

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command =>
            await _mediator.Send(command);
    }
}