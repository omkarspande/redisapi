using REDISAPI.Models;
using System.Collections.Generic;

namespace REDISAPI.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform plat);

        Platform? GetPlatformById(string id);

        IEnumerable<Platform> GetAllPlatforms();
    }
}