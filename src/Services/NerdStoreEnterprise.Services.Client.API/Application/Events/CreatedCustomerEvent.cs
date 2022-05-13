using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Events
{
    public class CreatedCustomerEvent : Event
    {
        public CreatedCustomerEvent(Guid id, string name, string email, string cpf)
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
    }
}