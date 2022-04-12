namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.Identity
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpiresAt { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}