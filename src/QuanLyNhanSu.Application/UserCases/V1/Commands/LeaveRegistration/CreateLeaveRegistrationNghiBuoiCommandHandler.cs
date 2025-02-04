using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Application.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveRegistration;
public sealed class CreateLeaveRegistrationNghiBuoiCommandHandler : ICommandHandler<Command.CreateLeaveRegistrationNghiBuoiCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly INotificationService _notificationService;

    public CreateLeaveRegistrationNghiBuoiCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository
,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
        _leaveDateRepository = leaveDateRepository;
        _notificationService = notificationService;
    }
    public async Task<Result> Handle(Command.CreateLeaveRegistrationNghiBuoiCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        var leaveDate = await _leaveDateRepository.FindAll(x => x.Id == request.LeaveDateId).ToListAsync();

        if (leaveDate.All(x => x.IsHoliday == true))
        {
            throw new Exception(" là ngày nghỉ lễ không thể chọn làm ngày xin nghỉ phép được.");
        }

        if (request.DayOff.DayOfWeek == DayOfWeek.Sunday || request.DayOff.DayOfWeek == DayOfWeek.Saturday)
        {
            throw new Exception("Đây là ngày nghỉ không thể xin nghỉ buổi.");
        }

        var leaveRegistration = Domain.Entities.LeaveRegistration.CreateLeaveRegistrationTypeOfLeaveNghiBuoi
        (
            Guid.NewGuid(),
            request.HalfDayOff,
            request.DayOff,
            request.LeaveReason,
            employeeId,
            request.LeaveDateId
        );

        var title = "Yêu cầu xin nghỉ buổi";
        var roles = new List<string> { "ADMIN", "DEPARTMENT_MANAGER" };
        var url = "/manage-Leaveregistration-halfdayoff";

        // Tạo thông báo
        var message = $"Nhân viên " +
            $"{employee.Select(x => x.Name).FirstOrDefault()} " +
            $"đã đăng ký nghỉ buổi {request.HalfDayOff} ngày {request.DayOff.ToString("dd/MM/yyyy")}.";

        // Gửi thông báo
        await _notificationService.SendNotificationToAdmins(message, roles, url, title);

        _leaveRegistrationRepository.Add(leaveRegistration);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
