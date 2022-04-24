using System;
using NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects;

namespace NerdStoreEnterprise.Services.Client.API.Models
{
    public class Client : Entity, IAggregateRoot
    {
        protected Client()
        {
        }

        public Client(Guid id, string name, string email, string cpf) : base(id)
        {
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Deleted = false;
        }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Deleted { get; private set; }
        public Address Address { get; private set; }

        public void AssignAddress(Address address) => Address = address;

        public void ChangeEmail(string email) => Email = new Email(email);

        public void Delete()
        {
            if (Deleted) throw new InvalidOperationException("User is already marked as deleted.");

            Deleted = true;
        }
    }
}