using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Exceptions;
using QuanLyNhanSu.Application.Abstractions;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Approvel;
public sealed class ApprovelAttendanceCommandHandler : ICommandHandler<Command.ApproveAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly INotificationService _notificationService;

    public ApprovelAttendanceCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor
,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(Command.ApproveAttendanceCommand request, CancellationToken cancellationToken)
    {
        #region ====== Kiểm tra quyền đăng nhập ======

        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        #endregion

        var attendance = await _attendanceRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        attendance.ApproveAttendance
        (
            request.PendingApproval,
            employeeId.Value,
            request.ApprovalNote,
            request.IsLate,
            request.IsEarlyLeave
        );

        #region ====== Tạo thông báo ======

        var employeeIdNoti = attendance.EmployeeId;
        var message = "";
        var url = "/attendance";
        var title = "";

        // Tạo thông báo
        if (request.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Daduyet)
        {
            title = "Chấm công của bạn đã được duyệt";
            message = $"Chấm công ngày {attendance.CreatedDate.Value.ToString("dd/MM/yyyy")} đã được quản lý chấp nhận.";
            // Gửi thông báo
            await _notificationService.SendNotificationToUsers(employeeIdNoti.Value, message, url, title);
        }

        if (request.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Tuchoi)
        {
            title = "Quản lý từ chối chấm công của bạn";
            message = $"Chấm công ngày {attendance.CreatedDate.Value.ToString("dd/MM/yyyy")} đã bị từ chối.";
            // Gửi thông báo
            await _notificationService.SendNotificationToUsers(employeeIdNoti.Value, message, url, title);
        }

        #endregion

        _attendanceRepository.Update(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
