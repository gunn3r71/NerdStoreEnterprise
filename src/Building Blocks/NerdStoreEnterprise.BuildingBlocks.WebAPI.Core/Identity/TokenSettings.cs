namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.Identity
{
    public class TokenSettings
    {
        /// <summary>
        /// Secret key for token decryption 
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token validity
        /// </summary>
        public int ExpiresAt { get; set; }

        /// <summary>
        /// Token issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token audience
        /// </summary>
        public string Audience { get; set; }
    }
}