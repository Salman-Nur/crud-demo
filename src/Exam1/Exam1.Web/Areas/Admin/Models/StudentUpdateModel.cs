using Autofac;
using Azure;
using Exam1.Application.Features.Admission.Services;
using Exam1.Domain.Entity;

namespace Exam1.Web.Areas.Admin.Models
{
    public class StudentUpdateModel
    {
        private IStudentManagementService _studentManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Age { get; set; }
        public uint Fees { get; set; }
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
            if(student is not null)
            {
                Name = student.Name;
                Description = student.Description;
                Age = student.Age;
                Fees = student.Fees;
            }
        }
        internal async Task UpdateStudentAsync()
        {
            await _studentManagementService.UpdateStudentAsync(Id, Name, Description, Age, Fees);
        }
    }
}
