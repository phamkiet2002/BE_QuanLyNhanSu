using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveRegistration;
public sealed class CreateLeaveRegistrationNghiNgayCommandHandler : ICommandHandler<Command.CreateLeaveRegistrationNghiNgayCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateLeaveRegistrationNghiNgayCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(Command.CreateLeaveRegistrationNghiNgayCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active).FirstOrDefaultAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        var leaveRegistration = Domain.Entities.LeaveRegistration.CreateLeaveRegistrationTypeOfLeaveNghiNgay
        (
            Guid.NewGuid(),
            request.StartDate,
            request.EndDate,
            request.LeaveReason,
            employeeId,
            request.LeaveDateId
        );

        _leaveRegistrationRepository.Add(leaveRegistration);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
