using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Persistence;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public DbSet<PositionRole> PositionRoles { get; set; }

    public DbSet<AllowanceAndPenalties> AllowanceAndPenalties { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<AttendanceSetting> AttendanceSettings { get; set; }
    public DbSet<Domain.Entities.Contract> Contracts { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    public DbSet<EmployeeLevel> EmployeeLevels { get; set; }
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<LeaveDate> LeaveDates { get; set; }
    public DbSet<LeaveRegistration> LeaveRegistrations { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<WorkPlace> WorkPlaces { get; set; }
    public DbSet<WorkPlaceAllowanceAndPenalties> WorkPlaceAllowanceAndPenalties { get; set; }
    public DbSet<WorkShedule> WorkShedules { get; set; }
    public DbSet<PayRoll> PayRolls { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}
