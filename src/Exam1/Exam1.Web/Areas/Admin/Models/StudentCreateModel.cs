using Autofac;
using Exam1.Application.Features.Admission.Services;

namespace Exam1.Web.Areas.Admin.Models
{
    public class StudentCreateModel
    {
        private ILifetimeScope _scope;
        private IStudentManagementService _studentManagementService;
        public string Name { get; set; }
        public uint Fees { get; set; }
        public double CGPA { get; set; }
        public StudentCreateModel()
        {
        }
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
            await _studentManagementService.CreateStudentAsync(Name, Fees, CGPA);
        }
    }
}
