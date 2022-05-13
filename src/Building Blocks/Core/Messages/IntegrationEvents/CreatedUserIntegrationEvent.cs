using System;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents
{
    public class CreatedUserIntegrationEvent : IntegrationEvent
    {
        public CreatedUserIntegrationEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Cpf { get; }
    }
}