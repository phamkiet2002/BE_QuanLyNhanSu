using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class UpdateEmployeePositionCommandHandler : ICommandHandler<Command.UpdateEmployeePositionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeePosition, Guid> _employeePositionRepository;

    public UpdateEmployeePositionCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<EmployeePosition, Guid> employeePositionRepository
    )
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _employeePositionRepository = employeePositionRepository;
    }

    public async Task<Result> Handle(Command.UpdateEmployeePositionCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        if (employee.Status == Domain.Enumerations.StatusEnums.Status.InActive)
            throw new Exception("Nhân viên đã nghỉ việc không thể cập nhật.");

        var employeePosition = await _employeePositionRepository.FindAll(x => x.EmployeeId == employee.Id).ToListAsync();


        foreach (var item in employeePosition)
        {
            if (item.Status == Domain.Enumerations.StatusEnums.Status.Active && item.PositionId != request.PositionId)
            {
                item.UpdateEmployeePosition(Domain.Enumerations.StatusEnums.Status.InActive, DateTime.Now);
                _employeePositionRepository.Update(item);
            }

            if (item.PositionId == request.PositionId)
            {
                item.UpdateEmployeePosition(Domain.Enumerations.StatusEnums.Status.Active, null);
                _employeePositionRepository.Update(item);
            }
        }

        if (employeePosition.All(x => x.PositionId != request.PositionId))
        {
            var newEmployeePosition = EmployeePosition.CreateEmployeePosition(employee.Id, request.PositionId);
            _employeePositionRepository.Add(newEmployeePosition);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
