
using Autofac;
using University.Domain.Features.Admission;

namespace University.Web.Areas.Admin.Models
{
    public class StudentCreateModel
    {
        private ILifetimeScope _scope;
        private IStudentManagementService _studentManagementService;
        public string Name { get; set; }
        public double Fees { get; set; }

        public StudentCreateModel() { }

        public StudentCreateModel(IStudentManagementService studentManagementService)
        {
            _studentManagementService = studentManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _studentManagementService = _scope.Resolve<IStudentManagementService>();
        }

        internal async Task CreateStudentAsync()
        {
            await _studentManagementService.CreateStudentAsync(Name, Fees);
        }
    }
}
