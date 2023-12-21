using University.Domain;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public virtual void Dispose() => _dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();
        public virtual async Task SaveAsync() => await _dbContext?.SaveChangesAsync();
    }
}
