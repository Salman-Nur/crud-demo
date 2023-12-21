using University.Domain.Entities;

namespace University.Domain.Features.Admission
{
    public interface IStudentManagementService
    {
        Task CreateStudentAsync(string name, double fees);

        Task<(IList<Student> records, int total, int totalDisplay)>
            GetPagedStudentsAsync(int pageIndex, int pageSize, string searchText,
            string sortBy);
    }
}