using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class LeaveWorkEmployeeCommandHandler : ICommandHandler<Command.LeaveWorkEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LeaveWorkEmployeeCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(Command.LeaveWorkEmployeeCommand request, CancellationToken cancellationToken)
    {
        Guid employeeId;

        if (request.Id == Guid.Empty)
        {
            var user = _httpContextAccessor.HttpContext?.User
                ?? throw new UnauthorizedAccessException("Người dùng không xác thực.");

            var userId = _userManager.GetUserId(user);
            var appUser = await _userManager.FindByIdAsync(userId)
                ?? throw new Exception("Người dùng không tồn tại.");

            employeeId = appUser.EmployeeId
                ?? throw new Exception("Người dùng không liên kết với nhân viên.");
        }
        else
        {
            employeeId = request.Id;
        }

        var employee = await _employeeRepository.FindByIdAsync(employeeId)
            ?? throw new EmployeeException.EmployeeNotFoundException(employeeId);

        if (employee.Status == Domain.Enumerations.StatusEnums.Status.InActive)
            throw new Exception("Nhân viên đã nghỉ việc không thể cập nhật.");

        employee.LeaveWorkEmployee();
        _employeeRepository.Update(employee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
