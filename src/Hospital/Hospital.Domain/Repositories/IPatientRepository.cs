using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Repositories
{
    public interface IPatientRepository : IRepositoryBase<Patient, Guid>
    {
        Task<(IList<Patient> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchName, double searchAgeFrom, double searchAgeTo, 
            string orderBy, int pageIndex, int pageSize);
    }
}
