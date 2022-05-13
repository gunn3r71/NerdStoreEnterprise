namespace NerdStoreEnterprise.Services.Customer.API.Infrastructure.Services
{
    //TODO -> abstrair para uma lib
    public class Email
    {
        public Email(string subject, string message, bool isHtml)
        {
            Subject = subject;
            Message = message;
            IsHtml = isHtml;
        }

        public string Subject { get; }
        public string Message { get; }
        public bool IsHtml { get; }
    }
}