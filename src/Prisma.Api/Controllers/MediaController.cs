using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Prisma.Api.Services;
using Prisma.Core;

namespace Prisma.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IMediaProvider<Media> _provider;
        private readonly IMediaStorageService _mediaStorageService;

        public MediaController(ILogger<MediaController> logger, IMediaProvider<Media> provider,
            IMediaStorageService mediaStorageService)
        {
            _logger = logger;
            _provider = provider;
            _mediaStorageService = mediaStorageService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Media>> Get(string name)
        {
            var movies = await _provider.GetAllByName(name);
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string url)
        {
            BackgroundJob.Enqueue(() => _mediaStorageService.StoreAsync(url));
            return Ok();
        }
    }
}
