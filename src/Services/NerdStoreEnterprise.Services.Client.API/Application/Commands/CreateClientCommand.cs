using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;
using NerdStoreEnterprise.Services.Client.API.Application.Validations;

namespace NerdStoreEnterprise.Services.Client.API.Application.Commands
{
    public class CreateClientCommand : Command
    {
        public CreateClientCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateClientCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}