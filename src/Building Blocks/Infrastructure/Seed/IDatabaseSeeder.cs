using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Infrastructure.Seed
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
        void Seed();
    }
}