using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient, Guid>, IPatientRepository
    {
        public PatientRepository(IApplicationDbContext context) : base((DbContext)context)
        {
            
        }
        public Task<(IList<Patient> records, int total, int totalDisplay)> GetTableDataAsync(string searchName, double searchAgeFrom, double searchAgeTo, string orderBy, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
