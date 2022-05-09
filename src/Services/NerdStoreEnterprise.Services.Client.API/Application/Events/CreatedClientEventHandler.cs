using MediatR;
using NerdStoreEnterprise.Services.Client.API.Infrastructure.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Services.Client.API.Application.Events
{
    public class CreatedClientEventHandler : INotificationHandler<CreatedClientEvent>
    {
        private readonly IEmailService _emailService;

        public CreatedClientEventHandler(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public Task Handle(CreatedClientEvent notification, CancellationToken cancellationToken)
        {
            var subject = "Welcome to Nerd Store Enterprise";
            var message = "<p>We want you to have an excellent experience with us.<br/> The Nerd Store Enterprise team appreciates your trust.</p>";

            var email = new Email(subject, message, true);

            _emailService.SendEmailAsync(email, notification.Email);

            return Task.CompletedTask;
        }
    }
}