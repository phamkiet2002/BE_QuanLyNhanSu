using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Exceptions;
using QuanLyNhanSu.Application.Abstractions;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Approvel;
public sealed class ApprovelLeaveRegistrationCommandHandler : ICommandHandler<Command.ApproveLeaveRegistrationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly INotificationService _notificationService;

    public ApprovelLeaveRegistrationCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,
        INotificationService notificationService
    )
    {
        _unitOfWork = unitOfWork;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(Command.ApproveLeaveRegistrationCommand request, CancellationToken cancellationToken)
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

        var leaveRegistration = await _leaveRegistrationRepository.FindByIdAsync(request.Id)
           ?? throw new LeaveRegistrationException.LeaveRegistrationNotFound(request.Id);

        if (leaveRegistration.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Dahuy)
            throw new Exception("không thể duyệt vì nhân viên đã hủy yêu cầu.");

        leaveRegistration.ApproveLeaveRegistration
        (
            employeeId.Value,
            request.ApprovalNote,
            request.PendingApproval
        );

        #region ====== Gửi thông báo ======

        var employeeIdNoti = leaveRegistration.EmployeeId;
        var message = "";
        var url = "/leave-registration";
        var title = "";

        // Tạo thông báo
        if (request.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Daduyet)
        {
            title = "Yêu cầu xin nghỉ đã được duyệt";
            message = $"Yêu cầu xin nghỉ ngày {leaveRegistration.CreatedDate.Value.ToString("dd/MM/yyyy")} đã được quản lý chấp nhận.";
            // Gửi thông báo
            await _notificationService.SendNotificationToUsers(employeeIdNoti.Value, message, url, title);
        }

        if (request.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Tuchoi)
        {
            title = "Quản lý từ chối bạn nghỉ phép";
            message = $"Yêu cầu xin nghỉ ngày {leaveRegistration.CreatedDate.Value.ToString("dd/MM/yyyy")} đã bị từ chối.";
            // Gửi thông báo
            await _notificationService.SendNotificationToUsers(employeeIdNoti.Value, message, url, title);
        }

        #endregion

        _leaveRegistrationRepository.Update(leaveRegistration);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
