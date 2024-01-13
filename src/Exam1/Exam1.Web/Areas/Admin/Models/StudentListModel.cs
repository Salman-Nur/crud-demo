using Autofac;
using Exam1.Application.Features.Admission.Services;
using Exam1.Infrastructure;
using System.Web;

namespace Exam1.Web.Areas.Admin.Models
{
    public class StudentListModel
    {
        private ILifetimeScope _scope;
        private IStudentManagementService _studentManagementService;
        public StudentSearch SearchItem { get; set; }
        public StudentListModel()
        {
        }
        public StudentListModel(IStudentManagementService studentManagementService)
        {
            _studentManagementService = studentManagementService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _studentManagementService = _scope.Resolve<IStudentManagementService>();
        }
        public async Task<object> GetPagedStudentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _studentManagementService.GetPagedStudentsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                SearchItem.Name,
                SearchItem.StudentFeesFrom,
                SearchItem.StudentFeesTo,
                dataTablesUtility.GetSortText(new string[] { "Name", "Fees", "CGPA" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.Name),
                            record.Fees.ToString(),
                            record.CGPA.ToString(),
                            record.Id.ToString()
                        }
                        ).ToArray()
            };
        }
        internal async Task DeleteStudentAsync(Guid id)
        {
            await _studentManagementService.DeleteStudentAsync(id);
        }
    }
}
