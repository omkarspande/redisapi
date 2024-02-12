using Microsoft.AspNetCore.Mvc;
using REDISAPI.Data;
using REDISAPI.Models;
using System.Collections.Generic;

namespace REDISAPI.AddControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : Controller
    {
        private readonly IPlatformRepo _platformRepo;

        public PlatformsController(IPlatformRepo platformRepo)
        {
            _platformRepo = platformRepo;
        }


        [HttpGet("{id}",Name ="GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = _platformRepo.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(platform);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(_platformRepo.GetAllPlatforms());
        }

    }
}