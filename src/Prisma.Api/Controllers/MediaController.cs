using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prisma.Api.Services;
using Prisma.Core;

namespace Prisma.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly ProviderSelector _selector;
        private readonly IMediaStorageService _mediaStorageService;

        public MediaController(ILogger<MediaController> logger, ProviderSelector selector,
            IMediaStorageService mediaStorageService)
        {
            _logger = logger;
            _selector = selector;
            _mediaStorageService = mediaStorageService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Media>> Get(string name, string providerName = "yts")
        {
            var provider =_selector.SelectProvider(providerName);

            if (provider is null)
                return BadRequest();

            var movies = await provider.GetAllByName(name);
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
