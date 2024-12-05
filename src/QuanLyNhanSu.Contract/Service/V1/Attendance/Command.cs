using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance;

public static class Command 
{
    //public record CheckInAttendanceCommand(Guid? EmployeeId) : ICommand;
    public record CheckInAttendanceCommand() : ICommand;
    //public record CheckOutAttendanceCommand(Guid Id, Guid? EmployeeId) : ICommand;
    public record CheckOutAttendanceCommand() : ICommand;
}
