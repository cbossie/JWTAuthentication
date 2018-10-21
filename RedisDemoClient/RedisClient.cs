using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;


namespace RedisDemoClient
{
    public class RedisClient : IDisposable
    {
        private ConnectionMultiplexer Conn { get; set; }
        private RedisConfiguration _config;
        private ISerializer _serializer;

        private StackExchangeRedisCacheClient ObjectClient { get; set; }



        protected virtual StackExchangeRedisCacheClient CacheClient { get; set; }



        public RedisClient(RedisConfiguration config, ISerializer serializer)
        {
            _config = config;
            _serializer = serializer;
            Connect();
            if (serializer != null)
            {
                InitializeCacheClient(serializer, _config.ConfigOptions.DefaultDatabase.GetValueOrDefault());
            }
        }









        protected void InitializeCacheClient(ISerializer serializer, int defaultDatabase)
        {
            CacheClient = new StackExchangeRedisCacheClient(Conn, serializer, defaultDatabase);

        }



        private void Connect()
        {
            Conn = ConnectionMultiplexer.Connect(_config.ConfigOptions);

        }

        private void Disconnect()
        {
            if ((Conn?.IsConnected).GetValueOrDefault())
            {
                Conn?.Close();
            }
        }

        private IDatabase Db => Conn?.GetDatabase();

        public bool PutValue(string key, int value)
        {
            return Db.StringSet(key, value);
        }

        public bool GetValue(string key, out string value)
        {
            value = null;
            var val = Db.StringGet(key);
            if (val.HasValue)
            {
                value = val;
            }
            return val.HasValue;
        }

        public bool AddObject<T>(string key, T value)
        {
            if (CacheClient == null)
            {
                return false;
            }

            return CacheClient.Add(key, value);
        }

        public bool AddObjectToList<T>(string key, T value) where T : class
        {
            if (CacheClient == null)
            {
                return false;
            }
            var result = CacheClient.SetAdd(key, value);
            return result;
        }


        public bool AddObjectsToList<T>(string key, IEnumerable<T> value) where T : class
        {
            if (CacheClient == null || value == null)
            {
                return false;
            }

            var result = CacheClient.SetAddAll(key, value.ToArray());
            return true;
        }

        public IEnumerable<T> GetListOfObjects<T>(string key) where T : class

        {
            if (CacheClient == null)
            {
                return Enumerable.Empty<T>();
            }

            var n = CacheClient.SetMembers<T>(key);
            return n;
        }

        public bool GetObject<T>(string key, out T value) where T : class
        {
            value = null;
            if (CacheClient == null)
            {
                return false;
            }

            try
            {
                var r = CacheClient.Get<T>(key);
                if (r != null)
                {
                    value = r;
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                value = null;
            }

            return false;
        }

        public async Task<T> GetObjectAsync<T>(string key) where T : class
        {
            T value = null;
            if (CacheClient == null)
            {
                return null;
            }

            try
            {
                var r = await CacheClient.GetAsync<T>(key);
                if (r != null)
                {
                    value = r;
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                value = null;
            }

            return null;
        }


        public long PublishToChannel<T>(string channel, T message) where T : class
        {
            if (CacheClient == null)
            {
                return 0l;
            }

            return CacheClient.Publish(channel, message);
        }

        public bool SubscribeToChannel<T>(string channel, Action<T> handler)
        {
            if (CacheClient == null)
            {
                return false;
            }
            CacheClient.Subscribe(channel, handler);
            return true;
        }

        public bool SubscribeToNotificationChannel(string channel, Action<RedisChannel, RedisValue> action)
        {
            if (!Conn.IsConnected || action == null)
            {
                return false;
            }
            Conn.GetSubscriber().Subscribe(channel, action);
            return true;
        }

        public bool SubscribeToKeySpaceChannel(string keypattern, Action<string, string> action, params string[] allowedEvents)
        {
            var channel = $"__keyspace@*__:{keypattern}";
            return SubscribeToNotificationChannel(channel, (c, v) =>
            {
                // If no allowed events are specified, then just take any. Otherwise only take the ones requested
                if (allowedEvents?.Length == 0 || allowedEvents.Any(a => a == v))
                {
                    KeyspaceChannelProcessor(c, v, action);
                }
            });
        }

        void KeyspaceChannelProcessor(RedisChannel channel, RedisValue value, Action<string, string> action)
        {
            var key = channel.ToString()?.Split(':');
            if (key.Length > 0 && key.Last() != null)
            {
                action?.Invoke(key.Last(), $"{value}");
            }
        }


        public void Dispose()
        {
            Disconnect();
        }
    }
}
