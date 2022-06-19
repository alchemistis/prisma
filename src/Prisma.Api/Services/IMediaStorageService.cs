namespace Prisma.Api.Services
{
    public interface IMediaStorageService
    {
        Task StoreAsync(string url);
    }
}
