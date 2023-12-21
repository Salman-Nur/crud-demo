
using Autofac;
using Hospital.Domain.Features.Accounts;

namespace Hospital.Web.Areas.Admin.Models
{
    public class PatientCreateModel
    {
        private ILifetimeScope _scope;
        private IPatientService _patientService;
        public string Name { get; set; }
        public double Bill { get; set; }

        public PatientCreateModel() { }

        public PatientCreateModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _patientService = _scope.Resolve<IPatientService>();
        }

        internal async Task CreatePatientAsync()
        {
            await _patientService.CreatePatientAsync(Name, Bill);
        }
    }
}
