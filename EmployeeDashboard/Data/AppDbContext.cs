using EmployeeDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }

        DbSet<Employee> Employees { get; set; }
    }
}
