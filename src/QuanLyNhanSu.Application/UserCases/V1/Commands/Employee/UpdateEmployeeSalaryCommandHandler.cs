using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class UpdateEmployeeSalaryCommandHandler : ICommandHandler<Command.UpdateEmployeeSalaryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.Salary, Guid> _salaryRepository;

    public UpdateEmployeeSalaryCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Salary, Guid> salaryRepository
    )
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _salaryRepository = salaryRepository;
    }

    public async Task<Result> Handle(Command.UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        if (employee.Status == Domain.Enumerations.StatusEnums.Status.InActive)
            throw new Exception("Nhân viên đã nghỉ việc không thể cập nhật.");

        var salary = await _salaryRepository.FindAll(x => x.EmployeeId == employee.Id).ToListAsync();

        foreach (var item in salary)
        {
            if (item.Status == Domain.Enumerations.StatusEnums.Status.Active)
            {
                item.UpdateSalary(Domain.Enumerations.StatusEnums.Status.InActive, DateTime.Now);
                _salaryRepository.Update(item);
            }
        }

        var newSalary = Domain.Entities.Salary.CreateSalary(Guid.NewGuid(), request.Salarys, employee.Id);
        _salaryRepository.Add(newSalary);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
