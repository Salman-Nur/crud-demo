using University.Domain;
using University.Domain.Repositories;

namespace University.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
    }
}
