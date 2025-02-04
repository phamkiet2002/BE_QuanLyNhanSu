using QuanLyNhanSu.Contract.Abstractions.Message;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance;

public static class Command
{
    // employee
    public record CheckInAttendanceCommand() : ICommand;
    public record CheckOutAttendanceCommand() : ICommand;

    // hangfire
    public record CheckIsAbsentAttendanceCommand() : ICommand;

    // admin
    public record CreateCheckInCheckOutAttendanceCommand(DateTime CheckIn, DateTime CheckOut, string ApprovalNote, Guid EmployeeId) : ICommand;
    public record ApproveAttendanceCommand(Guid Id, PendingApproval? PendingApproval, string? ApprovalNote, bool IsLate, bool IsEarlyLeave) : ICommand;
    public record OvertimeOutsideWorkHourAttendanceCommand(Guid Id, DateTime StartTime, DateTime EndTime) : ICommand;
    public record UpdateAttendanceCommand(Guid Id, DateTime CheckIn, DateTime CheckOut, string ReasonNote) : ICommand;
    public record UpdateIsAbsentAttendanceCommand(Guid Id, bool IsAbsent) : ICommand;
}
