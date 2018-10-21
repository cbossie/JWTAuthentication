using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace RedisDemoClient
{
    public class RedisConfiguration
    {
        public ConfigurationOptions ConfigOptions { get; private set; }

        private RedisConfiguration()
        {

        }

        public static RedisConfiguration CreateFromString(string connString, int? databaseId = null)
        {
            RedisConfiguration config = new RedisConfiguration();
            ConfigurationOptions opt = ConfigurationOptions.Parse(connString);
            config.ConfigOptions = opt;

            if (databaseId.HasValue)
            {
                config.ConfigOptions.DefaultDatabase = databaseId.Value;
            }

            return config;
        }

    }
}
