using MonoTorrent;
using MonoTorrent.Client;

namespace Prisma.Application.Torrent
{
    public class TorrentClient
    {
        private readonly ClientEngine _clientEngine;

        public TorrentClient()
        {
            _clientEngine = new ClientEngine();
        }

        public async Task DownloadAsync(string torrentFilePath)
        {
            var manager = await _clientEngine.AddAsync(torrentFilePath, Path.Combine(Environment.CurrentDirectory, "Downloads"));

            manager.TorrentStateChanged += async (s, e) =>
            {
                Console.WriteLine($"Torrent State Changed: {e.NewState}.");

                if (e.NewState == TorrentState.Downloading)
                {
                    await Task.Run(async () =>
                    {
                        while (manager.Progress < 100)
                        {
                            Console.WriteLine($"{manager.Torrent.Name} progress: {string.Format("{0:F1}", manager.Progress)}%");
                            await Task.Delay(10000);
                        }
                    });
                }

                if (e.NewState == TorrentState.Seeding)
                    await manager.StopAsync();
            };

            await manager.StartAsync();
        }
    }
}
