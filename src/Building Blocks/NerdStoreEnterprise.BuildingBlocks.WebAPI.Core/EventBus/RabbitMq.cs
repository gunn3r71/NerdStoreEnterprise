namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.EventBus
{
    public class RabbitMq
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string VHost { get; set; }
        public string Password { get; set; }
    }
}