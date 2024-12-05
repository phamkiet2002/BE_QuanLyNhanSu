using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.WorkShedule;
public static class Command
{
    public record CreateWorkSheduleCommand(string Name, TimeSpan StartTime, TimeSpan EndTime, TimeSpan BreakStartTime, TimeSpan BreakEndTime) : ICommand;
    public record UpdateWorkSheduleCommand(Guid Id, string Name, TimeSpan StartTime, TimeSpan EndTime, TimeSpan BreakStartTime, TimeSpan BreakEndTime) : ICommand;
    public record DeleteWorkSheduleCommand(Guid Id) : ICommand;
}
