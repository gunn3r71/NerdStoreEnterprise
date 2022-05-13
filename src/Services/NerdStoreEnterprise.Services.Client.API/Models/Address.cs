using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Customer.API.Models
{
    public class Address : Entity
    {
        private Address()
        {
        }

        public Address(string streetName, string buildingNumber, string addressComplement, string zipCode, string city, string state) : this()
        {
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            AddressComplement = addressComplement;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public string StreetName { get; private set; }
        public string BuildingNumber { get; private set; }
        public string AddressComplement { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
    }
}