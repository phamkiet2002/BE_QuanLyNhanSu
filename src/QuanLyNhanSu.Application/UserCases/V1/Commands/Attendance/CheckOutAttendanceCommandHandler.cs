using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Exceptions;
using QuanLyNhanSu.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class CheckOutAttendanceCommandHandler : ICommandHandler<Command.CheckOutAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> _attendanceSettingRepository;
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CheckOutAttendanceCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository,
        IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository,
        IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository
,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
        _attendanceSettingRepository = attendanceSettingRepository;
        _departmentRepository = departmentRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(Command.CheckOutAttendanceCommand request, CancellationToken cancellationToken)
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

        var departmentWithWorkSheduleEndTimes = await _departmentRepository.FindAll()
        .Include(d => d.WorkShedule)
        .Include(d => d.EmployeeDepartments)
        .Where(d => d.EmployeeDepartments.Any(ed => ed.EmployeeId == employeeId && ed.Status == Domain.Enumerations.StatusEnums.Status.Active))
        .Select(d => d.WorkShedule.EndTime)
        .ToListAsync(cancellationToken);

        TimeSpan? attendanceSetting = _attendanceSettingRepository
            .FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.IsDeleted != true).Select(x => x.MaxEarlyLeaveAllowed).FirstOrDefault();

        var departmentWithWorkSheduleEndTime = departmentWithWorkSheduleEndTimes.FirstOrDefault();

        var attendance = _attendanceRepository.FindAll().Where(x => x.EmployeeId == employeeId && x.CheckIn.Date == DateTime.Today).FirstOrDefault();

        var checkOutAttendance = await _attendanceRepository.FindAll(x => x.EmployeeId == employeeId).ToListAsync();

        if (checkOutAttendance.Any(x => x.CheckOut.Day == date.Day))
        {
            throw new Exception("bạn đã checkOut rồi.");
        }

        var updateAttendance = await _attendanceRepository.FindByIdAsync(attendance.Id)
            ?? throw new AttendanceException.AttendanceNotFoundException(attendance.Id);

        var timeAllowedToLeaveLarly = departmentWithWorkSheduleEndTime - attendanceSetting;

        // check out sớm hơn thời gian cho phép về sớm 
        if (date.TimeOfDay < attendanceSetting || date.TimeOfDay < departmentWithWorkSheduleEndTime)
        {
            var attendanceToday = _attendanceRepository.FindAll()
            .Where(x => x.EmployeeId == employeeId && x.CheckIn.Date == DateTime.Today)
            .FirstOrDefault();

            if (updateAttendance.CheckIn != null)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    updateAttendance.CheckOutAttendance
                    (
                        false,
                        null,
                        employeeId
                    );
                    _attendanceRepository.Update(updateAttendance);
                }

                var earlyLeaveTime = (attendanceSetting ?? departmentWithWorkSheduleEndTime) - DateTime.Now.TimeOfDay;
                var dateEarlyLeaveTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                           .Add(earlyLeaveTime);

                updateAttendance.CheckOutAttendance
                (
                    true,
                    dateEarlyLeaveTime,
                    employeeId
                );
                _attendanceRepository.Update(updateAttendance);
            }
        }
        else
        {
            updateAttendance.CheckOutAttendance
            (
                false,
                null,
                employeeId
            );
            _attendanceRepository.Update(updateAttendance);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
