

namespace University.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
