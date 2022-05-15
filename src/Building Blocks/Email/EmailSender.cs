using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NerdStoreEnterprise.BuildingBlocks.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly EmailSenderConfig _emailSenderConfig;

        public EmailSender(ISendGridClient sendGridClient,
                           IOptions<EmailSenderConfig> emailSenderConfig)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
            _emailSenderConfig = emailSenderConfig.Value;
        }

        public async Task SendEmailAsync(Email email) => 
            await _sendGridClient.SendEmailAsync(BuildMessage(email));

        public async Task SendEmailAsync(Email email, CancellationToken cancellationToken) => 
            await _sendGridClient.SendEmailAsync(BuildMessage(email), cancellationToken);

        private SendGridMessage BuildMessage(Email email)
        {
            var from = new EmailAddress(_emailSenderConfig.From, _emailSenderConfig.FromName);
            var tos = email.To.Select(e => new EmailAddress(e)).ToList();

            var message = new SendGridMessage()
            {
                From = from,
                Subject = email.Subject,
                HtmlContent = email.IsHtml ? email.Message : null, 
                PlainTextContent= !email.IsHtml ? email.Message : null
            };

            message.AddTos(tos);

            return message;
        }
    }
}