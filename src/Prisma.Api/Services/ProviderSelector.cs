using Prisma.Core;
using Prisma.Providers.Yts;
using Prisma.Providers.Rarbg;

namespace Prisma.Api.Services
{
    public class ProviderSelector
    {
        public Dictionary<string, IMediaProvider<Media>> Providers = new();

        public ProviderSelector(IConfiguration configuration, HttpClient httpClient)
        {
            Providers.Add("yts", new YtsProvider(configuration["Providers:Yts"]));
            Providers.Add("rarbg", new RarbgProvider(configuration["Providers:Rarbg"], httpClient));
        }

        public IMediaProvider<Media>? SelectProvider(string providerName)
        {
            if (Providers.TryGetValue(providerName, out var provider))
                return provider;

            return null;
        }
    }
}
