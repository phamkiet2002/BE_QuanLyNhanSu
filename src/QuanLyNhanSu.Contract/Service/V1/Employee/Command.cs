using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Domain.Enumerations.EmployeeEnums;

namespace QuanLyNhanSu.Contract.Service.V1.Employee;

public static class Command
{
    public record CreateEmployeeCommand
    (
        string Name, string Email, string Phone, string IdentityCard, Gender Gender,
        DateTime DateOfBirth, string Address, DateTime JoinDate, string BankName,
        string BankAccountNumber, Guid? WorkPlaceId, Guid? DepartmentId, Guid? LevelId, Guid? PositionId, decimal Salarys
    ) : ICommand;
    public record DeleteEmployeeCommand(Guid Id) : ICommand;
    public record UpdateEmployeeCommand
    (
        Guid Id, string Name, string Email, string Phone, string IdentityCard,
        Gender Gender, DateTime DateOfBirth, string Address, string BankName, string BankAccountNumber
    ) : ICommand;
    public record UpdateEmployeeLevelCommand(Guid Id, Guid? LevelId) : ICommand;
    public record UpdateEmployeeWorkPlaceCommand(Guid Id, Guid? WorkPlaceId, Guid? DepartmentId) : ICommand;
    public record UpdateEmployeePositionCommand(Guid Id, Guid? PositionId) : ICommand;
    public record UpdateEmployeeSalaryCommand(Guid Id, decimal Salarys) : ICommand;
    public record UpdateEmployeeDepartmentCommand(Guid Id, Guid? DepartmentId) : ICommand;
    public record LeaveWorkEmployeeCommand(Guid Id) : ICommand;

}
