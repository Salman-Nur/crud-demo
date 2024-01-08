using Exam1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.Admission.Services
{
    public interface IStudentManagementService
    {
        Task CreateStudentAsync(string name, string description, double age, uint fees);
        Task DeleteStudentAsync(Guid id);
        Task<Student> GetStudentAsync(Guid id);
        Task UpdateStudentAsync(Guid id, string name, string description, double age, uint fees);
        Task<(IList<Student> records, int total, int totalDisplay)>
            GetPagedStudentsAsync(int pageIndex, int pageSize, string searchName,
            uint searchFeesFrom, uint searchFeesTo, string sortBy);
    }
}
