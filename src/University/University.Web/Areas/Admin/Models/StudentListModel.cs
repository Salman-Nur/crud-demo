using University.Domain.Features.Admission;
using University.Infrastructure;
using System.Web;

namespace University.Web.Areas.Admin.Models
{
    public class StudentListModel
    {
        private readonly IStudentManagementService _studentService;

        public StudentListModel()
        {
        }

        public StudentListModel(IStudentManagementService studentService)
        {
            _studentService = studentService;
        }

        public async Task<object> GetPagedStudentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _studentService.GetPagedStudentsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Name", "Fees" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.Name),
                                record.Fees.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
