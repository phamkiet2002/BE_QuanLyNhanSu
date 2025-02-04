using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Domain.Enumerations.LeaveRegistrationEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class CheckIsAbsentAttendanceCommandHandler : ICommandHandler<Command.CheckIsAbsentAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkShedule, Guid> _workShedulerepositoryBase;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;

    public CheckIsAbsentAttendanceCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository,
        IRepositoryBase<Domain.Entities.WorkShedule, Guid> workShedulerepositoryBase,
        IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository
    )
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _workShedulerepositoryBase = workShedulerepositoryBase;
        _leaveDateRepository = leaveDateRepository;
    }

    public async Task<Result> Handle(Command.CheckIsAbsentAttendanceCommand request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;

        var employees = await _employeeRepository
            .FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active)
            .Include(x => x.Attendances)
            .Include(x => x.LeaveRegistrations)
            .Include(x => x.EmployeeDepartments)
            .ToListAsync(cancellationToken);

        var employeesWithoutAttendance = employees
            .Where(e => !e.Attendances.Any(a =>
                a.CheckIn.HasValue && a.CheckIn.Value.Date == today &&
                a.CheckOut.HasValue && a.CheckOut.Value.Date == today))
            .ToList();

        var leaveRegistration = _leaveRegistrationRepository
                .FindAll(x =>
                        x.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Daduyet &&
                        x.CreatedDate.Value.Month == DateTime.Now.Month)
                .ToList();

        foreach (var emp in employees)
        {
            var workShedule = await _workShedulerepositoryBase
            .FindAll(x => x.IsDeleted != true)
            .Include(x => x.Departments).Where(x => x.Departments.Any(d => d.EmployeeDepartments.Any(x => x.EmployeeId == emp.Id)))
            .ToListAsync(cancellationToken);

            var startTime = workShedule
                .Select(x => new DateTime(today.Year, today.Month, today.Day, x.StartTime.Hours, x.StartTime.Minutes, x.StartTime.Seconds))
                .FirstOrDefault();

            var endTime = workShedule
                .Select(x => new DateTime(today.Year, today.Month, today.Day, x.EndTime.Hours, x.EndTime.Minutes, x.EndTime.Seconds))
                .FirstOrDefault();

            var startBreakTime = workShedule
                .Select(x => new DateTime(today.Year, today.Month, today.Day, x.EndTime.Hours, x.EndTime.Minutes, x.EndTime.Seconds))
                .FirstOrDefault();

            var endBreakTime = workShedule
                .Select(x => new DateTime(today.Year, today.Month, today.Day, x.EndTime.Hours, x.EndTime.Minutes, x.EndTime.Seconds))
                .FirstOrDefault();

            var halfDayCount = leaveRegistration
               .Count(x => x.HalfDayOff == HalfDayOff.Sang || x.HalfDayOff == HalfDayOff.Chieu);
            var dayOff = leaveRegistration
                .Where(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .Sum(x =>
                {
                    DateTime startDate = DateTime.Parse(x.StartDate.Value.ToString()).Date;
                    DateTime endDate = DateTime.Parse(x.EndDate.Value.ToString()).Date;
                    if (startDate == endDate)
                        return 1;
                    return (endDate - startDate).Days + 1;
                });
            var totalLeaveDateCount = halfDayCount * 0.5 + dayOff;

            var totalAnnualLeaveDate = _leaveDateRepository
                .FindAll(x => x.IsDeleted != true).AsEnumerable().Where(x => leaveRegistration.Any(lr => lr.LeaveDateId == x.Id))
                .ToList();

            var hasLeaveTodayNghiNgay = leaveRegistration.Where(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .Any(lr => lr.EmployeeId == emp.Id && lr.StartDate.Value.Date <= today.Date && lr.EndDate.Value.Date >= today.Date);

            var hasLeaveTodayNghiBuoi = leaveRegistration.Where(x => x.DayOff.HasValue)
                .Any(lr => lr.EmployeeId == emp.Id && lr.DayOff.Value.Date == today.Date);

            if (hasLeaveTodayNghiNgay && totalAnnualLeaveDate.Any(x => x.TotalAnnualLeaveDate < (int)totalLeaveDateCount))
            {
                var leaveRequest = Domain.Entities.Attendance.CreateCheckInCheckOutAttendance(
                    Guid.NewGuid(),
                    startTime,
                    endTime,
                    null,
                    null,
                    null,
                    null,
                    true,
                    null,
                    emp.Id
                );
                _attendanceRepository.Add(leaveRequest);
            }
            else if (hasLeaveTodayNghiBuoi && totalAnnualLeaveDate.Any(x => x.TotalAnnualLeaveDate < (int)totalLeaveDateCount))
            {
                if (leaveRegistration.Any(x => x.HalfDayOff.Value == Domain.Enumerations.LeaveRegistrationEnums.HalfDayOff.Sang))
                {
                    var leaveRequest = Domain.Entities.Attendance.CreateCheckInCheckOutAttendance(
                        Guid.NewGuid(),
                        startTime,
                        startBreakTime,
                        null,
                        null,
                        null,
                        null,
                        true,
                        null,
                        emp.Id
                    );
                    _attendanceRepository.Add(leaveRequest);
                }
                if (leaveRegistration.Any(x => x.HalfDayOff.Value == Domain.Enumerations.LeaveRegistrationEnums.HalfDayOff.Chieu))
                {
                    var leaveRequest = Domain.Entities.Attendance.CreateCheckInCheckOutAttendance(
                        Guid.NewGuid(),
                        startBreakTime,
                        endTime,
                        null,
                        null,
                        null,
                        null,
                        true,
                        null,
                        emp.Id
                    );
                    _attendanceRepository.Add(leaveRequest);
                }
            }
        }

        var holidays = _leaveDateRepository.FindAll(x => x.IsHoliday == true).ToList()
            .SelectMany(x =>
            {
                DateTime start = x.StartDate.Value;
                DateTime end = x.EndDate.Value;
                return Enumerable.Range(0, (end - start).Days + 1)
                                 .Select(offset => start.AddDays(offset));
            });


        if (!(today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Saturday || holidays.Contains(today)))
        {
            foreach (var emp in employeesWithoutAttendance)
            {
                var isAbsentAttendance = Domain.Entities.Attendance.CheckAbsent(
                        Guid.NewGuid(),
                        emp.Id
                    );
                _attendanceRepository.Add(isAbsentAttendance);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
