using Prisma.Application.Torrent;
using System.Net.Mime;

namespace Prisma.Api.Services
{
    public class MediaStorageService : IMediaStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly TorrentClient _torrentClient;

        public MediaStorageService(HttpClient httpClient, TorrentClient torrentClient)
        {
            _httpClient = httpClient;
            _torrentClient = torrentClient;
        }

        public async Task StoreAsync(string url)
        {
            var torrent = await DownloadTorrentFileAsync(url);
            await _torrentClient.DownloadAsync(torrent);
        }

        private async Task<string> DownloadTorrentFileAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            response.Content.Headers.TryGetValues("Content-Disposition", out IEnumerable<string>? dispositionList);
            var disposition = dispositionList!.First();
            var contentDisposition = new ContentDisposition(disposition);

            var contentStream = response.Content.ReadAsStream();

            var fileName = contentDisposition?.FileName ?? $"Untitled-{DateTime.Now}.torrent";

            var targetPath = Path.Combine("Media", fileName);

            using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.ReadWrite);
            contentStream.CopyTo(fs);

            return targetPath;
        }
    }
}
