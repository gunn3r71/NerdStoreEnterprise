using System.Threading;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
        Task SendEmailAsync(Email email, CancellationToken cancellationToken);
    }
}