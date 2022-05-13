using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;

namespace NerdStoreEnterprise.Services.Customer.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
    }
}