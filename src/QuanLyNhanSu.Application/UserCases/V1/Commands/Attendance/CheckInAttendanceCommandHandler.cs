using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class CheckInAttendanceCommandHandler : ICommandHandler<Command.CheckInAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> _attendanceSettingRepository;
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckInAttendanceCommandHandler
    (
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository, IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository,
        IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository
    )
    {
        _attendanceRepository = attendanceRepository;
        _unitOfWork = unitOfWork;
        _attendanceSettingRepository = attendanceSettingRepository;
        _departmentRepository = departmentRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(Command.CheckInAttendanceCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId).FirstOrDefaultAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");


        var date = DateTime.Now;

        var departmentWithWorkSheduleStartTimes = await _departmentRepository.FindAll()
        .Include(d => d.WorkShedule)
        .Include(d => d.EmployeeDepartments)
        .Where(d => d.EmployeeDepartments.Any(ed => ed.EmployeeId == employeeId && ed.Status == Domain.Enumerations.StatusEnums.Status.Active))
        .Select(d => d.WorkShedule.StartTime)
        .ToListAsync(cancellationToken);

        var attendanceSetting = _attendanceSettingRepository
            .FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.IsDeleted != true).Select(x => x.MaximumLateAllowed).FirstOrDefault();

        var departmentWithWorkSheduleStartTime = departmentWithWorkSheduleStartTimes.FirstOrDefault();

        var checkInAttendance = await _attendanceRepository.FindAll(x => x.EmployeeId == employeeId).ToListAsync();

        if (checkInAttendance.Any(x=> x.CheckIn.Day == date.Day))
        {
            throw new Exception("bạn đã checkin rồi.");
        }


        if (departmentWithWorkSheduleStartTime < date.TimeOfDay)
        {
            if (date.TimeOfDay > departmentWithWorkSheduleStartTime.Add(attendanceSetting))
            {
                var lateTime = date.TimeOfDay - departmentWithWorkSheduleStartTime.Add(attendanceSetting);
                var lateDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                           .Add(lateTime);
                var attendanceIsLate = Domain.Entities.Attendance.CheckInAttendance
                (
                    Guid.NewGuid(),
                    true,
                    lateDateTime,
                    employeeId
                );
                _attendanceRepository.Add(attendanceIsLate);
            }
            else
            {
                var attendance = Domain.Entities.Attendance.CheckInAttendance
                (
                    Guid.NewGuid(),
                    false,
                    null,
                    employeeId
                );
                _attendanceRepository.Add(attendance);
            }
        }
        else
        {
            throw new Exception("Chưa tới giờ làm việc.");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
