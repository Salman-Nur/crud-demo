using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Features.Account
{
    public interface IPatientManagementService
    {
        Task CreatePatientAsync(string name, double age, uint bill);
        Task DeletePatientAsync(Guid id);
        Task<Patient> GetPatientAsync(Guid id);
        Task UpdatePatientAsync(Guid id, string name, double age, uint bill);

        Task<(IList<Patient> records, int total, int totalDisplay)>
            GetPagedPatientsAsync(int pageIndex, int pageSize, string searchName, 
            double searchAgeFrom, double searchAgeTo, string sortBy);
    }
}
