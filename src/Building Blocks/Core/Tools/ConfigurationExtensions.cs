﻿using Microsoft.Extensions.Configuration;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Tools
{
    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("MessageQueueConnection")?[name];
        }
    }
}