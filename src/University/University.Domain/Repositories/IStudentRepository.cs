using University.Domain.Entities;

namespace University.Domain.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student, Guid>
    {
        Task<bool> IsNameDuplicateAsync(string name, Guid? id = null);

        Task<(IList<Student> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);

    }
}
