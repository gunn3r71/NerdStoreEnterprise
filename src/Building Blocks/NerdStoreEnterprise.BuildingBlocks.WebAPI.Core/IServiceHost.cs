using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core
{
    public interface IServiceHost
    {
        Task RunAsync();
    }
}