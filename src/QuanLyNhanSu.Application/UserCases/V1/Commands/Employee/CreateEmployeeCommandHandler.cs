using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Employee;
public sealed class CreateEmployeeCommandHandler : ICommandHandler<Command.CreateEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> _employeeDepartmentRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> _employeeLevelRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeePosition, Guid> _employeePositionRepository;
    private readonly IRepositoryBase<Domain.Entities.Salary, Guid> _salaryRepository;

    private readonly UserManager<AppUser> _userManager;


    public CreateEmployeeCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<EmployeeDepartment, Guid> employeeDepartmentRepository, IRepositoryBase<EmployeeLevel, Guid> employeeLevelRepository,
        IRepositoryBase<EmployeePosition, Guid> employeePositionRepository, UserManager<AppUser> userManager,
        IRepositoryBase<Salary, Guid> salaryRepository
    )
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _employeeDepartmentRepository = employeeDepartmentRepository;
        _employeeLevelRepository = employeeLevelRepository;
        _employeePositionRepository = employeePositionRepository;
        _userManager = userManager;
        _salaryRepository = salaryRepository;
    }

    public async Task<Result> Handle(Command.CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Domain.Entities.Employee.CreateEmployee
        (
            Guid.NewGuid(),
            request.Name, request.Email, request.Phone, request.IdentityCard, request.Gender,
            request.DateOfBirth, request.Address, request.JoinDate, request.BankName, request.BankAccountNumber,
            request.WorkPlaceId
        );
        _employeeRepository.Add(employee);

        var employeeDepartment = EmployeeDepartment.CreateEmployeeDepartment(employee.Id, request.DepartmentId);
        _employeeDepartmentRepository.Add(employeeDepartment);

        var employeePosition = EmployeePosition.CreateEmployeePosition(employee.Id, request.PositionId);
        _employeePositionRepository.Add(employeePosition);

        var employeeLevel = EmployeeLevel.CreateEmployeeLevel(employee.Id, request.LevelId);
        _employeeLevelRepository.Add(employeeLevel);

        var salary = Domain.Entities.Salary.CreateSalary
        (
            Guid.NewGuid(), 
            request.Salarys, 
            employee.Id
        );
        _salaryRepository.Add(salary);

        var normalizedUserName = RemoveDiacritics(request.Name.Replace(" ", ""));
        var user = new AppUser
        {
            UserName = normalizedUserName,
            Email = request.Email,
            EmployeeId = employee.Id
        };
        await _userManager.CreateAsync(user, "12345678");

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public string RemoveDiacritics(string input)
    {
        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var character in normalizedString)
        {
            // Chỉ thêm ký tự nếu nó không phải là ký tự dấu (kết thúc với ký tự chưa có dấu)
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(character);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
