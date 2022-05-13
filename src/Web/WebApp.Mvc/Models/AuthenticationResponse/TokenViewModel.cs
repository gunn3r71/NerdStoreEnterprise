using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Models.AuthenticationResponse
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenInformationViewModel UserTokenInformation { get; set; }

        public ErrorViewModel ErrorDetails { get; set; }
    }
}