using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Customer.API.Models
{
    public class Address : Entity
    {
        private Address()
        {
        }

        public Address(string streetName, string buildingNumber, string addressComplement, string zipCode, string city,
            string state) : this()
        {
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            AddressComplement = addressComplement;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public string StreetName { get; }
        public string BuildingNumber { get; }
        public string AddressComplement { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string State { get; }
        public virtual Guid CustomerId { get; protected set; }
        public virtual Customer Customer { get; protected set; }
    }
}