using Hospital.Domain;
using Hospital.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
    }
}
