using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class UpdateEmployeeDepartmentCommandHandler : ICommandHandler<Command.UpdateEmployeeDepartmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> _employeeDepartmentRepository;

    public UpdateEmployeeDepartmentCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository,
        IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> employeeDepartmentRepository
    )
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _employeeDepartmentRepository = employeeDepartmentRepository;
    }

    public async Task<Result> Handle(Command.UpdateEmployeeDepartmentCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        if (employee.Status == Domain.Enumerations.StatusEnums.Status.InActive)
            throw new Exception("Nhân viên đã nghỉ việc không thể cập nhật.");

        var department = await _departmentRepository.FindByIdAsync(request.DepartmentId.Value)
            ?? throw new DepartmentException.DepartmentNotFoundException(request.DepartmentId.Value);

        if (employee.WorkPlaceId != department.WorkPlaceId)
        {
            throw new Exception("điểm làm việc không có phòng ban này.");
        }

        var employeeDepartment = await _employeeDepartmentRepository.FindAll(x => x.EmployeeId == employee.Id).ToListAsync();

        foreach (var item in employeeDepartment)
        {
            if (item.Status == Domain.Enumerations.StatusEnums.Status.Active && item.DepartmentId != request.DepartmentId)
            {
                item.UpdateEmployeeDepartment(Domain.Enumerations.StatusEnums.Status.InActive, DateTime.Now);
                _employeeDepartmentRepository.Update(item);
            }

            if (item.DepartmentId == request.DepartmentId)
            {
                item.UpdateEmployeeDepartment(Domain.Enumerations.StatusEnums.Status.Active, null);
                _employeeDepartmentRepository.Update(item);
            }
        }

        if (employeeDepartment.All(x => x.DepartmentId != request.DepartmentId))
        {
            var newEmployeeDepartment = EmployeeDepartment.CreateEmployeeDepartment(employee.Id, request.DepartmentId);
            _employeeDepartmentRepository.Add(newEmployeeDepartment);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
