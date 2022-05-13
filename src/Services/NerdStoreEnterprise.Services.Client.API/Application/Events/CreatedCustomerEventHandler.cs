using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStoreEnterprise.Services.Customer.API.Infrastructure.Services;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Events
{
    public class CreatedCustomerEventHandler : INotificationHandler<CreatedCustomerEvent>
    {
        private readonly IEmailService _emailService;

        public CreatedCustomerEventHandler(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            var subject = "Welcome to Nerd Store Enterprise";
            var message = "<p>We want you to have an excellent experience with us.<br/> The Nerd Store Enterprise team appreciates your trust.</p>";

            var email = new Email(subject, message, true);

            _emailService.SendEmailAsync(email, notification.Email);

            return Task.CompletedTask;
        }
    }
}