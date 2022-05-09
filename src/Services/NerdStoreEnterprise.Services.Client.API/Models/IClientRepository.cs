using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;

namespace NerdStoreEnterprise.Services.Client.API.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        void Add(Client client);
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetByCpf(string cpf);
    }
}