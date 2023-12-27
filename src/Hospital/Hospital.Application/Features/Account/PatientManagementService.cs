using Hospital.Domain.Entities;
using Hospital.Domain.Features.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Account
{
    internal class PatientManagementService : IPatientManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public PatientManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreatePatientAsync(string name, double age, uint bill)
        {
            Patient patient = new Patient()
            {
                Name = name,
                Age = age,
                Bill = bill
            };
            _unitOfWork.PatientRepository.Add(patient);
            await _unitOfWork.SaveAsync();
        }

        public Task DeletePatientAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(IList<Patient> records, int total, int totalDisplay)> GetPagedPatientsAsync(int pageIndex, int pageSize, string searchName, double searchAgeFrom, double searchAgeTo, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePatientAsync(Guid id, string name, double age, uint bill)
        {
            throw new NotImplementedException();
        }
    }
}
