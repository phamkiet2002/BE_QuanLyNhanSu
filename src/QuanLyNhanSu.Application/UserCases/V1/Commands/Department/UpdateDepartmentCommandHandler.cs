using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Department;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Department;
public sealed class UpdateDepartmentCommandHandler : ICommandHandler<Command.UpdateDepartmentCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDepartmentCommandHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.FindByIdAsync(request.Id)
            ?? throw new DepartmentException.DepartmentNotFoundException(request.Id);
        department.UpdateDepartment(request.Name, request.Description, request.WorkPlaceId, request.WorkSheduleId);
        _departmentRepository.Update(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
