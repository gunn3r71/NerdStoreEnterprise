namespace NerdStoreEnterprise.WebApp.Mvc.Models.Users
{
    public record UserLoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}