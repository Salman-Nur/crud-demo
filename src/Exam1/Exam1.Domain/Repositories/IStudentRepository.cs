using Exam1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Repositories
{
    public  interface IStudentRepository : IRepositoryBase<Student, Guid>
    {
        Task<bool> IsNameDuplicateAsync(string name, Guid? id = null);
        Task<(IList<Student> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchName, uint searchFeesFrom, uint searchFeesTo, 
            string orderBy, int pageIndex, int pageSize);
    }
}
