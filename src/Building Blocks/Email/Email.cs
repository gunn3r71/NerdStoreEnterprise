namespace NerdStoreEnterprise.BuildingBlocks.EmailSender
{
    public class Email
    {
        public Email(string[] to, string subject, string message, bool isHtml)
        {
            To = to;
            Subject = subject;
            Message = message;
            IsHtml = isHtml;
        }
        
        public string[] To { get; }
        public string Subject { get; }
        public string Message { get; }
        public bool IsHtml { get; }
    }
}