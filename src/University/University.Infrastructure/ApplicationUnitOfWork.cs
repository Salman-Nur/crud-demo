using University.Application;
using University.Domain.Repositories;


namespace University.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IStudentRepository StudentRepository { get; private set; }

        public ApplicationUnitOfWork(IStudentRepository studentRepository, 
            ApplicationDbContext dbContext) : base(dbContext)
        {
            StudentRepository = studentRepository;
        }
    }
}
