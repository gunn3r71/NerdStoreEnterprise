namespace NerdStoreEnterprise.Services.Client.API.Infrastructure.Services
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
        
        public string Subject { get; private set; }
        public string Message { get; private set; }
        public bool IsHtml { get; private set; }
    }
}