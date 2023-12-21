using Hospital.Domain.Entities;

namespace Hospital.Domain.Features.Accounts
{
    public interface IPatientService
    {
        Task CreatePatientAsync(string name, double bill);

        Task<(IList<Patient> records, int total, int totalDisplay)>
            GetPagedPatientsAsync(int pageIndex, int pageSize, string searchText,
            string sortBy);
    }
}