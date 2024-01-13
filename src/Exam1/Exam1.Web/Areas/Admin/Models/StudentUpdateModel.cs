using Autofac;
using Exam1.Application.Features.Admission.Services;
using Exam1.Domain.Entities;

namespace Exam1.Web.Areas.Admin.Models
{
    public class StudentUpdateModel
    {
        private IStudentManagementService _studentManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint Fees { get; set; }
        public double CGPA { get; set; }
        public StudentUpdateModel()
        {
        }
        public StudentUpdateModel(IStudentManagementService studentManagementService)
        {
            _studentManagementService = studentManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _studentManagementService = scope.Resolve<IStudentManagementService>();
        }
        internal async Task LoadAsync(Guid id)
        {
            Student student = await _studentManagementService.GetStudentAsync(id);
            if (student != null)
            {
                Id = student.Id;
                Name = student.Name;
                Fees = student.Fees;
                CGPA = student.CGPA;
            }
        }
        internal async Task UpdateStudentAsync()
        {
            await _studentManagementService.UpdateStudentAsync(Id, Name, Fees, CGPA);
        }
    }
}
