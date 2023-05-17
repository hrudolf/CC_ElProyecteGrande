using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class DataContext : DbContext
{
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Roster> Rosters { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<VacationRequest> VacationRequests { get; set; }
    public DbSet<User> Users { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}