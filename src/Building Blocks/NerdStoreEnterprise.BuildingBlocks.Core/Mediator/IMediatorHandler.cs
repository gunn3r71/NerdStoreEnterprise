using System.Threading.Tasks;
using FluentValidation.Results;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T ev) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}