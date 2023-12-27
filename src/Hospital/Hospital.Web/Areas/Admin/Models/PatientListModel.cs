using Autofac;
using Hospital.Domain.Features.Account;
using Hospital.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Web;

namespace Hospital.Web.Areas.Admin.Models
{
    public class PatientListModel
    {
        private ILifetimeScope _scope;
        private IPatientManagementService _patientManagementService;
        public PatientSearch SearchItem { get; set; }
        public PatientListModel()
        {
        }
        public PatientListModel(IPatientManagementService patientManagementService)
        {
            _patientManagementService = patientManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _patientManagementService = _scope.Resolve<IPatientManagementService>();
        }
        public async Task<object> GetPagedPatientsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _patientManagementService.GetPagedPatientsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                SearchItem.Name,
                SearchItem.PatientAgeFrom,
                SearchItem.PatientAgeTo,
                dataTablesUtility.GetSortText(new string[] { "Name", "Age", "Bill" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                select new string[]
                {
                    HttpUtility.HtmlEncode(record.Name),
                    record.Age.ToString(),
                    record.Bill.ToString(),
                    record.Id.ToString(),
                }
                ).ToArray()
            };
        }
        internal async Task DeletePatientAsync(Guid id)
        {
            await _patientManagementService.DeletePatientAsync(id);
        }
    }
}
