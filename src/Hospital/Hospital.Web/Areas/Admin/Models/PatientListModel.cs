using Hospital.Domain.Features.Accounts;
using Hospital.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Web;

namespace Hospital.Web.Areas.Admin.Models
{
    public class PatientListModel
    {
        private readonly IPatientService _patientService;

        public PatientListModel()
        {
        }

        public PatientListModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<object> GetPagedPatientsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _patientService.GetPagedPatientsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Name", "Bill" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.Name),
                                record.Bill.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
