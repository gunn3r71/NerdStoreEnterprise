namespace NerdStoreEnterprise.Services.Identity.API.Extensions
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpiresAt { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}