using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class UpdateEmployeeCommandHandler : ICommandHandler<Command.UpdateEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(Command.UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var updateEmployee = await _employeeRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        updateEmployee.UpdateEmployee
        (
            request.Name,
            request.Email,
            request.Phone,
            request.IdentityCard,
            request.Gender,
            request.DateOfBirth,
            request.Address,
            request.BankName,
            request.BankAccountNumber
        );

        _employeeRepository.Update(updateEmployee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
