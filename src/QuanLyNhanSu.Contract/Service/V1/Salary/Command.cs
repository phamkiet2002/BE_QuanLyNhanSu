using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Salary;

public static class Command
{
    public record CreateSalaryCommand(decimal Salarys, Guid? EmployeeId) : ICommand;
}
