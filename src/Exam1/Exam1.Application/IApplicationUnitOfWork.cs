using Exam1.Domain.Repositories;
using Exam1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
    }
}
