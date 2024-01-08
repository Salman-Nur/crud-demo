using Exam1.Domain.Entities;
using Exam1.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Exam1.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
    }
}