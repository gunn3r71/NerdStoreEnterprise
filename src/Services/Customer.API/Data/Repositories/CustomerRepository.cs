using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;
using NerdStoreEnterprise.Services.Customer.API.Models;

namespace NerdStoreEnterprise.Services.Customer.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _context;

        public CustomerRepository(CustomersDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Models.Customer customer)
        {
            _context.Clients.Add(customer);
        }

        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Models.Customer> GetByCpf(string cpf)
        {
            return await _context.Clients.FirstOrDefaultAsync(client => client.Cpf.Number == cpf);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}