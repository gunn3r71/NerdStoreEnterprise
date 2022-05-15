using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Customer.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        protected Customer()
        {
        }

        public Customer(Guid id, string name, string email, string cpf) : base(id)
        {
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Deleted = false;
        }

        public string Name { get; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; }
        public bool Deleted { get; private set; }
        public Address Address { get; private set; }

        public void AssignAddress(Address address)
        {
            Address = address;
        }

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

        public void Delete()
        {
            if (Deleted) throw new InvalidOperationException("Customer is already marked as deleted.");

            Deleted = true;
        }
    }
}