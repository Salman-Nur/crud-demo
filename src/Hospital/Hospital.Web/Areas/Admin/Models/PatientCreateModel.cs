using Autofac;
using Hospital.Domain.Features.Account;

namespace Hospital.Web.Areas.Admin.Models
{
    public class PatientCreateModel
    {
        private ILifetimeScope _scope;
        private IPatientManagementService _patientManagementService;
        public string Name { get; set; }
        public double Age { get; set; }
        public uint Bill { get; set; }
        public PatientCreateModel()
        {
        }
        public PatientCreateModel(IPatientManagementService patientManagementService)
        {
            _patientManagementService = patientManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _patientManagementService = _scope.Resolve<IPatientManagementService>();
        }
        internal async Task CreateBookAsync()
        {
            await _patientManagementService.CreatePatientAsync(Name, Age, Bill);
        }
    }
}
