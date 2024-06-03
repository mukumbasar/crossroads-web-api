using Crossroads.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDatabase = StackExchange.Redis.IDatabase;

namespace Crossroads.Application.Services
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task<bool> AddDataAsync(string key, string value)
        {
            IDatabase db = _redis.GetDatabase();
            return await db.StringSetAsync(key, value);
        }

        public async Task<Dictionary<string, string>> GetDataAsync(string keyPrefix)
        {
            IDatabase db = _redis.GetDatabase();
            var result = new Dictionary<string, string>();

            var server = _redis.GetServer(_redis.GetEndPoints().First());

            var keys = server.Keys(pattern: keyPrefix + "*").ToArray();

            var tasks = keys.Select(async key =>
            {
                var value = await db.StringGetAsync(key);
                return new { Key = key.ToString(), Value = value.ToString() };
            });

            var keyValuePairs = await Task.WhenAll(tasks);

            foreach (var kvp in keyValuePairs)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    result.Add(kvp.Key, kvp.Value);
                }
            }

            return result;
        }
    }
}
