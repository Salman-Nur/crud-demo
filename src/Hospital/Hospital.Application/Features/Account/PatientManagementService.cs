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

        public async Task DeletePatientAsync(Guid id)
        {
            await _unitOfWork.PatientRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Patient> records, int total, int totalDisplay)> GetPagedPatientsAsync(
            int pageIndex, int pageSize, string searchName, double searchAgeFrom, double searchAgeTo, string sortBy)
        {
            return await _unitOfWork.PatientRepository.GetTableDataAsync(searchName, searchAgeFrom, searchAgeTo, 
                sortBy, pageIndex, pageSize);
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            return await _unitOfWork.PatientRepository.GetByIdAsync(id);
        }

        public async Task UpdatePatientAsync(Guid id, string name, double age, uint bill)
        {
            var patient = await GetPatientAsync(id);
            if (patient is not null)
            {
                patient.Name = name;
                patient.Age = age;
                patient.Bill = bill;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
