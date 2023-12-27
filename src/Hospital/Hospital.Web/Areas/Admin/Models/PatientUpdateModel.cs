using Autofac;
using Hospital.Domain.Entities;
using Hospital.Domain.Features.Account;

namespace Hospital.Web.Areas.Admin.Models
{
    public class PatientUpdateModel
    {
        private ILifetimeScope _scope;
        private IPatientManagementService _patientManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public uint Bill { get; set; }
        public PatientUpdateModel()
        {
        }
        public PatientUpdateModel(IPatientManagementService patientManagementService)
        {
            _patientManagementService = patientManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _patientManagementService = _scope.Resolve<IPatientManagementService>();
        }
        internal async Task LoadAsync(Guid id)
        {
            Patient patient = await _patientManagementService.GetPatientAsync(id);
            if(patient != null)
            {
                Id = patient.Id;
                Name = patient.Name;
                Age = patient.Age;
                Bill = patient.Bill;
            }
        }
        internal async Task UpdatePatientAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name) && Age >= 0)
            {
                await _patientManagementService.UpdatePatientAsync(Id, Name, Age, Bill);
            }
        }
    }
}
