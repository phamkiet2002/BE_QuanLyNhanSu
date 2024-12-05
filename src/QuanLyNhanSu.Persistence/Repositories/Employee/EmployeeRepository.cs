using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;

namespace QuanLyNhanSu.Persistence.Repositories.Employee;
public class EmployeeRepository : RepositoryBase<Domain.Entities.Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public double CalculateStandardWorkingDay(Domain.Entities.Employee employee)
    {
        //double totalWorkedHours = 0;

        //foreach (var attendance in employee.Attendances)
        //{
        //    if (attendance.CheckOut > attendance.CheckIn)
        //    {
        //        var workedTime = attendance.CheckOut.TimeOfDay - attendance.CheckIn.TimeOfDay;

        //        var lateTime = attendance.LateTime.Value.TimeOfDay;
        //        var earlyLeaveTime = attendance.EarlyLeaveTime.Value.TimeOfDay;

        //        var totalTime = workedTime - lateTime - earlyLeaveTime;

        //        totalWorkedHours += totalTime.TotalHours;
        //    }
        //}

        //return totalWorkedHours / 8;  // Assuming 8 hours workday

        return 22.0;
    }

    public decimal CalculateTotalAllowance(Domain.Entities.Employee employee)
    {
        //return employee.WorkPlace.WorkPlaceAndAllowanceAndPenalties
        //    .Where(x =>
        //            x.WorkPlace.IsDeleted != true &&
        //            x.AllowanceAndPenalties.IsDeleted != true &&
        //            x.AllowanceAndPenalties.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phucap)
        //    .Sum(x => x.AllowanceAndPenalties.Money);

        return employee.Salarys.Sum(s => s.Salarys);
    }

    public decimal CalculateTotalPenalties(Domain.Entities.Employee employee)
    {
        //return employee.WorkPlace.WorkPlaceAndAllowanceAndPenalties
        //    .Where(x =>
        //            x.WorkPlace.IsDeleted != true &&
        //            x.AllowanceAndPenalties.IsDeleted != true &&
        //            x.AllowanceAndPenalties.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phat)
        //    .Sum(x => x.AllowanceAndPenalties.Money);

        return 100.0m;
    }
}
