using University.Domain.Entities;
using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsNameDuplicateAsync(string name, Guid? id = null)
        {
            if (id.HasValue)
            {
                return (await GetCountAsync(x => x.Id != id.Value && x.Name == name)) > 0;
            }
            else
            {
                return (await GetCountAsync(x => x.Name == name)) > 0;
            }
        }

        public async Task<(IList<Student> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(x => x.Name.Contains(searchText),
                orderBy, null, pageIndex, pageSize, true);
        }
    }
}
