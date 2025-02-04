using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Persistence.Repositories.Employee;
public class EmployeeRepository : RepositoryBase<Domain.Entities.Employee, Guid>, IEmployeeRepository
{
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    public EmployeeRepository(ApplicationDbContext context, IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository) : base(context)
    {
        _attendanceRepository = attendanceRepository;
    }

    public double CalculateStandardWorkingDay(Domain.Entities.Employee employee)
    {
        double totalWorkedHours = 0;

        foreach (var attendance in employee.Attendances)
        {
            if (attendance.CheckOut > attendance.CheckIn && attendance.CreatedDate.Value.Month == DateTime.Now.Month)
            {
                var workedTime = attendance.CheckOut.Value.TimeOfDay - attendance.CheckIn.Value.TimeOfDay;

                var lateTime = attendance.LateTime?.TimeOfDay ?? TimeSpan.Zero;
                var earlyLeaveTime = attendance.EarlyLeaveTime?.TimeOfDay ?? TimeSpan.Zero;

                var totalTime = workedTime - lateTime - earlyLeaveTime;

                totalWorkedHours += totalTime.TotalHours;
            }
        }

        return totalWorkedHours / 8;
    }

    public decimal CalculateTotalAllowance(Domain.Entities.Employee employee)
    {
        return employee.WorkPlace.WorkPlaceAndAllowanceAndPenalties
            .Where(x =>
                    x.WorkPlaceId == employee.WorkPlaceId &&
                    x.WorkPlace.IsDeleted != true &&
                    x.AllowanceAndPenalties.IsDeleted != true &&
                    x.AllowanceAndPenalties.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phucap)
            .Sum(x => x.AllowanceAndPenalties?.Money ?? 0);
    }

    public decimal CalculateTotalPenalties(Domain.Entities.Employee employee)
    {
        var attendace = _attendanceRepository.FindAll(x => x.EmployeeId == employee.Id && x.CreatedDate.Value.Month == DateTime.Now.Month).ToList();
        var totalIsLate = attendace.Count(x => x.IsLate == true);

        var totalPenalties = 0m;
        if (totalIsLate > 0)
        {
            var isLate = employee.WorkPlace.WorkPlaceAndAllowanceAndPenalties
                .Where(x =>
                        x.WorkPlaceId == employee.WorkPlaceId &&
                        x.WorkPlace.IsDeleted != true &&
                        x.AllowanceAndPenalties.IsDeleted != true &&
                        x.AllowanceAndPenalties.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phat &&
                        x.AllowanceAndPenalties.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Dimuon)
                .Sum(x => x.AllowanceAndPenalties?.Money ?? 0);
            totalPenalties += totalIsLate * isLate;
        }

        var totalIsEarlyLeave = attendace.Count(x => x.IsEarlyLeave == true);
        if (totalIsEarlyLeave > 0)
        {
            var IsEarlyLeave = employee.WorkPlace.WorkPlaceAndAllowanceAndPenalties
                .Where(x =>
                        x.WorkPlaceId == employee.WorkPlaceId &&
                        x.WorkPlace.IsDeleted != true &&
                        x.AllowanceAndPenalties.IsDeleted != true &&
                        x.AllowanceAndPenalties.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phat &&
                        x.AllowanceAndPenalties.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Vesom)
                .Sum(x => x.AllowanceAndPenalties?.Money ?? 0);
            totalPenalties += totalIsLate * IsEarlyLeave;
        }

        return totalPenalties;
    }
}
