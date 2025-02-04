using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Department;

public static class Command 
{
    public record CreateDepartmentCommand(string Name, string Description, Guid? WorkPlaceId, Guid? WorkSheduleId) : ICommand;
    public record UpdateDepartmentCommand(Guid Id, string Name, string Description, Guid? WorkPlaceId, Guid? WorkSheduleId) : ICommand;
    public record DeleteDepartmentCommand(Guid Id) : ICommand;
}
