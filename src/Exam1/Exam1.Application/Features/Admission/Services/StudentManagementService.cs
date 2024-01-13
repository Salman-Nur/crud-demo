using Exam1.Domain.Entities;
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
        public async Task CreateStudentAsync(string name, uint fees, double cgpa)
        {
            bool isDuplicateName = await _unitOfWork.StudentRepository.IsNameDuplicateAsync(name);
            if (isDuplicateName)
            {
                throw new Exception();
            }
            Student student = new Student
            {
                Name = name,
                Fees = fees,
                CGPA = cgpa
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
            GetPagedStudentsAsync(int pageIndex, int pageSize, string searchName, 
            uint searchFeesFrom, uint searchFeesTo, string sortBy)
        {
            return await _unitOfWork.StudentRepository.GetTableDataAsync(searchName, searchFeesFrom,
                searchFeesTo, sortBy, pageIndex, pageSize);
        }

        public async Task<Student> GetStudentAsync(Guid id)
        {
            return await _unitOfWork.StudentRepository.GetByIdAsync(id);
        }

        public async Task UpdateStudentAsync(Guid id, string name, uint fees, double cgpa)
        {
            bool isDuplicateName = await _unitOfWork.StudentRepository.IsNameDuplicateAsync(name, id);
            if (isDuplicateName)
            {
                throw new Exception();
            }
            var student = await GetStudentAsync(id);
            if (student is not null)
            {
                student.Name = name;
                student.Fees = fees;
                student.CGPA = cgpa;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
