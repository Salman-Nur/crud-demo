using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient, Guid>, IPatientRepository
    {
        public PatientRepository(IApplicationDbContext context) : base((DbContext)context)
        {
            
        }
        public async Task<(IList<Patient> records, int total, int totalDisplay)> GetTableDataAsync(
            string searchName, double searchAgeFrom, double searchAgeTo, string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Patient, bool>> expression = null;
            if(!string.IsNullOrWhiteSpace(searchName))
            {
                expression = x => x.Name.Contains(searchName) &&
                (x.Age >= searchAgeFrom && x.Age <= searchAgeTo);
            }
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }
    }
}
