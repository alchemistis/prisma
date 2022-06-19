namespace Prisma.Core
{
    public interface IMediaProvider<T>
    {
        Task<IEnumerable<T>?> GetAllByName(string name);
    }
}
