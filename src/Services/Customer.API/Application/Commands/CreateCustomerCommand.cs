﻿using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;
using NerdStoreEnterprise.Services.Customer.API.Application.Validations;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Commands
{
    public class CreateCustomerCommand : Command
    {
        public CreateCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Cpf { get; }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}