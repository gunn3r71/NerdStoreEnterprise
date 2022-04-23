using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}