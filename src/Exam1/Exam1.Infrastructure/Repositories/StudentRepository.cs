using Exam1.Domain.Entities;
using Exam1.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(IApplicationDbContext context) : base((DbContext)context)
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
            GetTableDataAsync(string searchName, uint searchFeesFrom, uint searchFeesTo, 
            string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Student, bool>> expression = null;

            if(!string.IsNullOrWhiteSpace(searchName))
            {
                expression = x => x.Name.Contains(searchName) && 
                (x.Fees >= searchFeesFrom && x.Fees <= searchFeesTo);
            }
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

    }
}
