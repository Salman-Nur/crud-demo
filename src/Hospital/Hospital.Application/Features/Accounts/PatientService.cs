using Hospital.Domain.Entities;
using Hospital.Domain.Features.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Accounts
{
    public class PatientService : IPatientService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public PatientService(IApplicationUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public async Task CreatePatientAsync(string name, double bill)
        {
            bool isDuplicatName = await _unitOfWork.PatientRepository.
                IsNameDuplicateAsync(name);

            if (isDuplicatName)
                throw new InvalidOperationException();

            Patient patient = new Patient
            {
                Name = name,
                Bill = bill
            };

            _unitOfWork.PatientRepository.Add(patient);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Patient> records, int total, int totalDisplay)>
            GetPagedPatientsAsync(int pageIndex, int pageSize,
                string searchText, string sortBy)
        {
            return await _unitOfWork.PatientRepository.GetTableDataAsync(searchText, sortBy,
                pageIndex, pageSize);
        }
    }
}
