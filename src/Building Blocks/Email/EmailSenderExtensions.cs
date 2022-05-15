using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.BuildingBlocks.EmailSender
{
    public static class EmailSenderExtensions
    {
        public static void AddEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();

            var section = configuration.GetSection("EmailSender");

            services.Configure<EmailSenderConfig>(x => section.Bind(x));

            #region Configuring SendGrid Api Key

            var emailSenderConfig = new EmailSenderConfig();
            section.Bind(emailSenderConfig);

            services.AddSendGrid(options =>
            {
                options.ApiKey = emailSenderConfig.ApiKey;
            });

            #endregion

        }
    }
}