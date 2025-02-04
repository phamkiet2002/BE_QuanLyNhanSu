using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class UpdateEmployeeLevelCommandHandler : ICommandHandler<Command.UpdateEmployeeLevelCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> _employeeLevelRepository;

    public UpdateEmployeeLevelCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> employeeLevelRepository)
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _employeeLevelRepository = employeeLevelRepository;
    }

    public async Task<Result> Handle(Command.UpdateEmployeeLevelCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        if (employee.Status == Domain.Enumerations.StatusEnums.Status.InActive)
            throw new Exception("Nhân viên đã nghỉ việc không thể cập nhật.");

        var employeeLevel =await _employeeLevelRepository.FindAll(x => x.EmployeeId == employee.Id).AsNoTracking().ToListAsync();

        foreach (var item in employeeLevel)
        {
            if (item.Status == Domain.Enumerations.StatusEnums.Status.Active && item.LevelId != request.LevelId)
            {
                item.UpdateEmployeeLevel(Domain.Enumerations.StatusEnums.Status.InActive, DateTime.Now);
                _employeeLevelRepository.Update(item);
            }

            if (item.LevelId == request.LevelId)
            {
                item.UpdateEmployeeLevel(Domain.Enumerations.StatusEnums.Status.Active, null);
                _employeeLevelRepository.Update(item);
            }
        }

        if (employeeLevel.All(x => x.LevelId != request.LevelId))
        {
            var newEmployeeLevel = Domain.Entities.EmployeeLevel.CreateEmployeeLevel(employee.Id, request.LevelId);
            _employeeLevelRepository.Add(newEmployeeLevel);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
