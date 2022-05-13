using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}