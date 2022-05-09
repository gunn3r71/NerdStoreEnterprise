using System.Threading.Tasks;

namespace NerdStoreEnterprise.Services.Client.API.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email, string addressee);
        Task SendEmailAsync(Email email, string[] addressees);
    }
}