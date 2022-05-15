using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStoreEnterprise.BuildingBlocks.EmailSender;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Events
{
    public class CreatedCustomerEventHandler : INotificationHandler<CreatedCustomerEvent>
    {
        private readonly IEmailSender _emailService;

        public CreatedCustomerEventHandler(IEmailSender emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            var to = new [] {notification.Email};
            var subject = $"Welcome to Nerd Store Enterprise {notification.Name}";
            var message = $"<p>we want you to have an excellent experience with us.<br/> The Nerd Store Enterprise team appreciates your trust.</p>";

            var email = new Email(to ,subject, message, true);

            await _emailService.SendEmailAsync(email, cancellationToken);
        }
    }
}