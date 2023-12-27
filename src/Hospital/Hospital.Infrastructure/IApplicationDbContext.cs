using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Patient> Patients { get; set; }
    }
}