using REDISAPI.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace REDISAPI.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentOutOfRangeException(nameof(plat));
            }

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(plat);

            //db.StringSet(plat.Id, serialPlat);
            //db.SetAdd("PlatformSet", serialPlat);

            db.HashSet("Hashplatform", new HashEntry[] { new HashEntry(plat.Id, serialPlat) });
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            var db = _redis.GetDatabase();

            //var completeSet = db.SetMembers("PlatformSet");
            var completeSetHash = db.HashGetAll("Hashplatform");
            if (completeSetHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeSetHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

                return obj;
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            //var plat = db.StringGet(id);
            var plat = db.HashGet("Hashplatform", id);
            if (!String.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }

            return null;
        }
    }
}