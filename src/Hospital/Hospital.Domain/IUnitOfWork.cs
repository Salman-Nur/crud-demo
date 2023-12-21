using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
