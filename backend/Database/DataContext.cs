using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class DataContext : DbContext
{
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}