using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

using IDatabase = StackExchange.Redis.IDatabase;

namespace Crossroads.Application.Interfaces.Services
{
    public interface IRedisService
    {
        Task<bool> AddDataAsync(string key, string value);
        Task<Dictionary<string, string>> GetDataAsync(string keyPrefix);
    }
}
