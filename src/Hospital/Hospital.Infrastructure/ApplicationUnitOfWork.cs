using Hospital.Application;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IPatientRepository PatientRepository { get; private set; }

        public ApplicationUnitOfWork(IPatientRepository patientRepository, 
            IApplicationDbContext dbContext) : base((DbContext)dbContext)
        {
            PatientRepository = patientRepository;
        }
    }
}
