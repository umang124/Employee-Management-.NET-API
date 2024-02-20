using Employee_Management_System_Net_Core_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
