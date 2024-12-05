using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Department;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Department;
public sealed class CreateDepartmentCommandHandler : ICommandHandler<Command.CreateDepartmentCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentCommandHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = Domain.Entities.Department.CreateDepartment
        (
            Guid.NewGuid(), 
            request.Name, 
            request.Description, 
            request.WorkPlaceId, 
            request.WorkSheduleId
        );
        _departmentRepository.Add(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
