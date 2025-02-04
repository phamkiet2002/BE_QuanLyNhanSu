using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Application.Abstractions;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;
using QuanLyNhanSu.Domain.Entities;
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

    private readonly IRepositoryBase<Domain.Entities.WifiConfig, Guid> _wifiConfigRepositoryBase;
    private readonly IWifiCongfigRepository _wifiConfigRepository;


    private readonly INotificationService _notificationService;

    public CheckInAttendanceCommandHandler
    (
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository, IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository,
        IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        INotificationService notificationService,
        IWifiCongfigRepository wifiConfigRepository, 
        IRepositoryBase<Domain.Entities.WifiConfig, Guid> wifiConfigRepositoryBase
    )
    {
        _attendanceRepository = attendanceRepository;
        _unitOfWork = unitOfWork;
        _attendanceSettingRepository = attendanceSettingRepository;
        _departmentRepository = departmentRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
        _notificationService = notificationService;
        _wifiConfigRepository = wifiConfigRepository;
        _wifiConfigRepositoryBase = wifiConfigRepositoryBase;
    }

    public async Task<Result> Handle(Command.CheckInAttendanceCommand request, CancellationToken cancellationToken)
    {

        #region ============ kiểm tra quyền ============

        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId).ToListAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        #endregion

        var wifiConfig = await _wifiConfigRepositoryBase
            .FindAll(x => employee.Select(e => e.WorkPlaceId).Contains(x.WorkPlaceId))
            .ToListAsync();

        var wifiConfigInFor = _wifiConfigRepository.GetWiFiInfo()
            ?? throw new Exception("Không tìm thấy thông tin wifi");

        string bssid = _wifiConfigRepository.ExtractInfo(wifiConfigInFor, "BSSID\\s+:\\s+(.+)");

        if (wifiConfig.All(x=> x.BSSID != bssid))
            throw new Exception("Chưa cấu hình wifi chấm công này.");

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

        if (checkInAttendance.Any(x => x.CheckIn.HasValue && x.CheckIn.Value.Day == date.Day))
            throw new Exception("bạn đã checkin rồi.");

        if (date.DayOfWeek == DayOfWeek.Saturday)
            throw new Exception("Nay là thứ 7 không được checkIn");

        if (date.DayOfWeek == DayOfWeek.Sunday)
            throw new Exception("Nay là chủ nhật không được checkIn");

        var chophepditre = attendanceSetting != TimeSpan.Zero ? attendanceSetting : departmentWithWorkSheduleStartTime;

        if (departmentWithWorkSheduleStartTime < date.TimeOfDay)
        {
            if (date.TimeOfDay > chophepditre)
            {
                var lateTime = date.TimeOfDay - chophepditre;
                var lateDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                           .Add(lateTime);
                var attendanceIsLate = Domain.Entities.Attendance.CheckInAttendance
                (
                    Guid.NewGuid(),
                    true,
                    lateDateTime,
                    employeeId
                );

                #region ====== thông báo ======

                var title = "Đi trễ";
                var roles = new List<string> { "ADMIN", "DEPARTMENT_MANAGER, HR_MANAGER" };
                var url = "/manage-attendance-approval";

                // Tạo thông báo
                var message = $"Nhân viên " +
                    $"{employee.Select(x => x.Name).FirstOrDefault()} " +
                    $"đã đi trễ ngày {attendanceIsLate.CreatedDate.Value.ToString("dd/MM/yyyy")}.";

                // Gửi thông báo
                await _notificationService.SendNotificationToAdmins(message, roles, url, title);

                #endregion

                _attendanceRepository.Add(attendanceIsLate);
            }
            else
            {
                var attendance = Domain.Entities.Attendance.CheckInAttendance
                (
                    Guid.NewGuid(),
                    null,
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
