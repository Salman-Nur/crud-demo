using University.Domain.Entities;
using University.Domain.Features.Admission;


namespace University.Application.Features.Admission
{
    public class StudentManagementService : IStudentManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public StudentManagementService(IApplicationUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public async Task CreateStudentAsync(string name, double fees)
        {
            bool isDuplicatName = await _unitOfWork.StudentRepository.
                IsNameDuplicateAsync(name);

            if (isDuplicatName)
                throw new InvalidOperationException();

            Student student = new Student
            {
                Name = name,
                Fees = fees,
            };

            _unitOfWork.StudentRepository.Add(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Student> records, int total, int totalDisplay)>
            GetPagedStudentsAsync(int pageIndex, int pageSize,
                string searchText, string sortBy)
        {
            return await _unitOfWork.StudentRepository.GetTableDataAsync(searchText, sortBy,
                pageIndex, pageSize);
        }
    }
}
