namespace NerdStoreEnterprise.WebApp.Mvc.Models.Errors
{
    public class ErrorViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public ErrorMessagesViewModel Errors { get; set; }
    }
}