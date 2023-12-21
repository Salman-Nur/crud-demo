using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;

namespace University.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
    }
}