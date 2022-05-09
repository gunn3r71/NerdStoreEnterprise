using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;
using NerdStoreEnterprise.Services.Client.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Services.Client.API.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientsDbContext _context;

        public ClientRepository(ClientsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Models.Client client)
        {
            _context.Clients.Add(client);
        }

        public async Task<IEnumerable<Models.Client>> GetAll() =>
            await _context.Clients.AsNoTracking().ToListAsync();

        public async Task<Models.Client> GetByCpf(string cpf) =>
            await _context.Clients.FirstOrDefaultAsync(client => client.Cpf.Number == cpf);

        public void Dispose() => _context.Dispose();
    }
}