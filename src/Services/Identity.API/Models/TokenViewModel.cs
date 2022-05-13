namespace NerdStoreEnterprise.Services.Identity.API.Models
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenInformationViewModel UserTokenInformation { get; set; }
    }
}