using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Department;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Department;
public sealed class DeleteDepartmentCommandHandler : ICommandHandler<Command.DeleteDepartmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> _employeeDepartmentRepository;

    public DeleteDepartmentCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, 
        IRepositoryBase<EmployeeDepartment, Guid> employeeDepartmentRepository
    )
    {
        _unitOfWork = unitOfWork;
        _departmentRepository = departmentRepository;
        _employeeDepartmentRepository = employeeDepartmentRepository;
    }

    public async Task<Result> Handle(Command.DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.FindByIdAsync(request.Id)
            ?? throw new DepartmentException.DepartmentNotFoundException(request.Id);

        var employeeDepartments = await _employeeDepartmentRepository
            .FindAll(x => x.DepartmentId == department.Id && x.Employee.Status == Domain.Enumerations.StatusEnums.Status.Active).AnyAsync();

        if (employeeDepartments)
            throw new Exception("phòng ban này còn nhân viên vui lòng chuyển nhân viên trước khi xóa.");

        department.DeleteDepartment();
        _departmentRepository.Update(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
