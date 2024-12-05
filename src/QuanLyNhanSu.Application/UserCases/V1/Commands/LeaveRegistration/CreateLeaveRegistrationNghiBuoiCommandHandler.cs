using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveRegistration;
public sealed class CreateLeaveRegistrationNghiBuoiCommandHandler : ICommandHandler<Command.CreateLeaveRegistrationNghiBuoiCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateLeaveRegistrationNghiBuoiCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
    }
    public async Task<Result> Handle(Command.CreateLeaveRegistrationNghiBuoiCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active).FirstOrDefaultAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        var leaveRegistration = Domain.Entities.LeaveRegistration.CreateLeaveRegistrationTypeOfLeaveNghiBuoi
        (
            Guid.NewGuid(),
            request.HalfDayOff,
            request.DayOff,
            request.LeaveReason,
            employeeId,
            request.LeaveDateId
        );

        _leaveRegistrationRepository.Add(leaveRegistration);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
