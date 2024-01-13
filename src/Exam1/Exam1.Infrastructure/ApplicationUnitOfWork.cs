using Exam1.Application;
using Exam1.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IStudentRepository StudentRepository { get; private set; }

        public ApplicationUnitOfWork(IStudentRepository studentRepository, 
            IApplicationDbContext dbContext) : base((DbContext)dbContext)
        {
            StudentRepository = studentRepository;
        }

    }
}
