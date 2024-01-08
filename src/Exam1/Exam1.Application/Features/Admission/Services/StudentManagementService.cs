using Exam1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.Admission.Services
{
    public class StudentManagementService : IStudentManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public StudentManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateStudentAsync(string name, string description, double age, uint fees)
        {
            bool isNameDuplicate = await _unitOfWork.StudentRepository.IsNameDuplicateAsync(name);
            if(isNameDuplicate)
            {
                throw new Exception();
            }

            Student student = new Student
            {
                Name = name,
                Description = description,
                Age = age,
                Fees = fees
            };
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            await _unitOfWork.StudentRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Student> records, int total, int totalDisplay)> 
            GetPagedStudentsAsync(int pageIndex, int pageSize, string searchName, uint searchFeesFrom, uint searchFeesTo, string sortBy)
        {
            return await _unitOfWork.StudentRepository.GetTableDataAsync(searchName, searchFeesFrom, 
                searchFeesTo, sortBy, pageIndex, pageSize);
        }

        public async Task<Student> GetStudentAsync(Guid id)
        {
            return await _unitOfWork.StudentRepository.GetByIdAsync(id);
        }

        public async Task UpdateStudentAsync(Guid id, string name, string description, double age, uint fees)
        {
            bool isNameDuplicate = await _unitOfWork.StudentRepository.IsNameDuplicateAsync(name, id);
            if (isNameDuplicate)
            {
                throw new Exception();
            }
            var student = await GetStudentAsync(id);
            if(student is not null)
            {
                student.Name = name;
                student.Description = description;
                student.Age = age;
                student.Fees = fees;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
