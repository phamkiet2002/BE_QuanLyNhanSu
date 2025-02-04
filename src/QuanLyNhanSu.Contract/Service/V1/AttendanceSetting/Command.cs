using QuanLyNhanSu.Contract.Abstractions.Message;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
public static class Command
{
    public record CreateAttendanceSettingCommand(TimeSpan MaximumLateAllowed, TimeSpan MaxEarlyLeaveAllowed) : ICommand;
    public record UpdateAttendanceSettingCommand(Guid Id, TimeSpan MaximumLateAllowed, TimeSpan MaxEarlyLeaveAllowed) : ICommand;
    public record DeleteAttendanceSettingCommand(Guid Id) : ICommand;
}
