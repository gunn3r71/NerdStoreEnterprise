using System;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Services.Customer.API.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(Email email, string addressee)
        {
            Console.WriteLine($"Sending e-mail to {addressee}.");

            return Task.CompletedTask;
        }

        public Task SendEmailAsync(Email email, string[] addressees)
        {
            Console.WriteLine("Sending e-mail to: ");
            foreach (var addressee in addressees) Console.WriteLine(addressee);
            return Task.CompletedTask;
        }
    }
}