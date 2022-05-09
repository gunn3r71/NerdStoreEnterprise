using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;
using NerdStoreEnterprise.Services.Client.API.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using NerdStoreEnterprise.Services.Client.API.Application.Events;

namespace NerdStoreEnterprise.Services.Client.API.Application.Commands
{
    public class CreateClientCommandHandler : CommandHandler, IRequestHandler<CreateClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<ValidationResult> Handle(CreateClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            var existingClient = await _clientRepository.GetByCpf(client.Cpf.Number);

            if (existingClient is not null)
            {
                AddError("The CPF entered already exists.");
                return ValidationResult;
            }

            _clientRepository.Add(client);

            client.AddEvent(new CreatedClientEvent(client.Id, client.Name, client.Email.Address, client.Cpf.Number));

            return await PersistData(_clientRepository.UnitOfWork);
        }
    }
}