using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.Services.Client.API.Application.Events
{
    public class CreatedClientEvent : Event
    {
        public CreatedClientEvent(Guid id, string name, string email, string cpf)
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