namespace Prisma.Core
{
    public class Media
    {
        public string Name { get; set; }
        public string ProviderUrl { get; set; }
        public IEnumerable<string> DownloadLinks { get; set; }

        public Media(string name, string providerUrl, IEnumerable<string> links)
        {
            Name = name;
            ProviderUrl = providerUrl;
            DownloadLinks = links;
        }
    }
}
