using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}